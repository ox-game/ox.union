using OX.Ledger;
using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.IO;
using OX.VM.Types;
using OX.VM;
using VMArray = OX.VM.Types.Array;
using OX.Cryptography.ECC;
using OX.SmartContract;
using System.Runtime.CompilerServices;
using Nethereum.Model;

namespace OX.Tablet
{
    public static class TransactionHelper
    {
        public static ECPoint[] GetPublicKeys(this Transaction tx)
        {
            List<ECPoint> list = new List<ECPoint>();
            foreach (var witness in tx.Witnesses)
            {
                try
                {
                    var pubkey = ECPoint.FromBytes(witness.VerificationScript.Skip(1).Take(33).ToArray(), ECCurve.Secp256r1);
                    if (pubkey.IsNotNull())
                        list.Add(pubkey);
                }
                catch { }
            }
            return list.ToArray();
        }

        public static ECPoint GetBestWitnessPublicKey(this Transaction tx)
        {
            var pubkeys = tx.GetPublicKeys();
            if (pubkeys.IsNullOrEmpty()) return default;
            var pubkey = pubkeys.FirstOrDefault(p => tx.Outputs.Select(m => m.ScriptHash).Contains(Contract.CreateSignatureRedeemScript(p).ToScriptHash()));
            if (pubkey.IsNull())
            {
                pubkey = pubkeys.FirstOrDefault();
            }
            return pubkey;
        }
        public static ECPoint GetBestOriginPublicKey(this Transaction tx, IEnumerable<UInt160> exclunde)
        {
            ECPoint fromPubKey = tx.RelatedPublicKeys?.FirstOrDefault();
            if (fromPubKey.IsNotNull()) return fromPubKey;
            var pubkeys = tx.GetPublicKeys();
            if (pubkeys.IsNotNullAndEmpty())
            {
                var pubkey = pubkeys.FirstOrDefault(p => tx.Outputs.Select(m => m.ScriptHash).Contains(Contract.CreateSignatureRedeemScript(p).ToScriptHash()));
                if (pubkey.IsNotNull()) return pubkey;
                return pubkeys.FirstOrDefault(m => !exclunde.Contains(Contract.CreateSignatureRedeemScript(m).ToScriptHash()));
            }
            return default;
        }
        public static UInt160 GetBestOriginAddress(this Transaction tx, out string ethAddress)
        {
            ethAddress = string.Empty;
            if (tx.EthSignatures.IsNotNullAndEmpty() && tx.EthSignatures.Count() <= 2)
            {
                foreach (var sig in tx.EthSignatures)
                {
                    try
                    {
                        var stringToSign = tx.InputOutputHash.ToArray().ToHexString();
                        var signer = new Nethereum.Signer.EthereumMessageSigner();
                        var ethAddr = signer.EncodeUTF8AndEcRecover(stringToSign, sig.CreateStringSignature());
                        if (ethAddr.IsNotNullAndEmpty())
                        {
                            ethAddress = ethAddr;
                            return new EthereumMapTransaction { EthereumAddress = ethAddr }.GetContract().ScriptHash;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            var sh = tx.RelatedScriptHashes?.FirstOrDefault();
            if (sh.IsNotNull()) return sh;
            var pubkey = tx.GetBestWitnessPublicKey();
            if (pubkey.IsNotNull()) return Contract.CreateSignatureRedeemScript(pubkey).ToScriptHash();
            var relatedPubkey = tx.RelatedPublicKeys?.FirstOrDefault();
            if (relatedPubkey.IsNotNull()) return Contract.CreateSignatureRedeemScript(relatedPubkey).ToScriptHash();
            return tx.Witnesses?.Select(m => m.ScriptHash)?.FirstOrDefault();
        }
    }
}
