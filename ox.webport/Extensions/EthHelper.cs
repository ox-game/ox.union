using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using OX.IO;
using OX.Network.P2P.Payloads;
using System.Runtime.CompilerServices;
using OX.Ledger;
using OX.Casino;
using OX.SmartContract;
using OX.Cryptography.ECC;

namespace OX.WebPort
{

    public static class EthHelper
    {

        public static string Omit(this string address, int limitLength = 6)
        {
            var length = address.Length;
            if (length > limitLength*2+3)
            {
                return address.Substring(0, limitLength) + "..." + address.Substring(length - limitLength, limitLength);
            }
            return address;
        }
        public static string OmitOnlyLeft(this string address, int limitLength = 6)
        {
            var length = address.Length;
            if (length > limitLength+3)
            {
                return address.Substring(0, limitLength) + "...";
            }
            return address;
        }
        public static string OmitOnlyRight(this string address, int limitLength = 6)
        {
            var length = address.Length;
            if (length > limitLength + 3)
            {
                return "..." + address.Substring(length - limitLength, limitLength);
            }
            return address;
        }
        public static bool IsOnlyFromEthereumMapAddress(this Transaction tx, out string ethAddress)
        {
            ethAddress = string.Empty;
            foreach (var witness in tx.Witnesses)
            {
                try
                {
                    using (CenterExecutionEngine engine = new CenterExecutionEngine(tx))
                    {
                        engine.LoadScript(witness.VerificationScript);
                        engine.LoadScript(witness.InvocationScript);
                        engine.Execute();
                        if (engine.pubkey.IsNotNullAndEmpty())
                        {
                            if (witness.ScriptHash == Contract.CreateSignatureRedeemScript(ECPoint.DecodePoint(engine.pubkey, ECCurve.Secp256r1)).ToScriptHash())
                            {
                                return false;
                            }
                        }
                    }
                }
                catch { }
            }
            if (tx.Attributes.IsNotNullAndEmpty())
            {
                var attrs = tx.Attributes.Where(p => p.Usage == TransactionAttributeUsage.EthSignature);
                if (attrs.IsNotNullAndEmpty())
                {
                    var message = tx.InputOutputHash.ToArray().ToHexString();
                    var signer = new Nethereum.Signer.EthereumMessageSigner();
                    foreach (var attr in attrs)
                    {
                        try
                        {
                            var signature = Encoding.UTF8.GetString(attr.Data);
                            var address = signer.EncodeUTF8AndEcRecover(message, signature);
                            if (address.IsNotNullAndEmpty())
                            {
                                ethAddress = address;
                                return true;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            return false;
        }
    }
}
