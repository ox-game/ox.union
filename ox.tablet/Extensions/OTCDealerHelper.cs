using OX.Cryptography.ECC;
using OX.IO;
using System.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using System.Linq;
using Nethereum.Model;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using Akka.IO;
using OX.Wallets;
using System.Collections.Generic;
//using System.Runtime.InteropServices.WindowsRuntime;

namespace OX.Tablet
{
    public static class OTCDealerHelper
    {
        public static SlotSideTransaction BuildOTCDealerTransaction(this string ethAddress, OTCSetting setting = default)
        {
            byte[] attch = setting.IsNotNull() ? setting.ToArray() : new byte[0];
            SlotSideTransaction st = new SlotSideTransaction
            {
                Slot = Mining.LockMiningAccountPubKey,
                Channel = 0x01,
                SideType = SideType.EthereumAddress,
                Data = ethAddress.HexToByteArray(),
                Flag = 0,
                AuthContract = Blockchain.SideAssetContractScriptHash,
                Attach = attch
            };
            return st;
        }
        public static bool VerifyOTCDealerTx(this SlotSideTransaction st, out string ethAddress, out OTCSetting setting)
        {
            ethAddress = string.Empty;
            setting = default;
            if (
                st.Flag == 0
                && st.Channel == 0x01
                && st.SideType == SideType.EthereumAddress
                && st.Slot.Equals(Mining.LockMiningAccountPubKey)
                && st.AuthContract == Blockchain.SideAssetContractScriptHash
                && st.EthSignatures.IsNotNullAndEmpty())
            {
                try
                {
                    if (st.Attach.IsNotNullAndEmpty())
                    {
                        setting = st.Attach.AsSerializable<OTCSetting>();
                        if (setting.InRate > 20 || setting.OutRate > 20) return false;

                    }
                    var addr = new AddressUtil().ConvertToChecksumAddress(st.Data.ToHex());
                    var message = st.InputOutputHash.ToArray().ToHexString();
                    var signer = new Nethereum.Signer.EthereumMessageSigner();
                    foreach (var sig in st.EthSignatures)
                    {
                        try
                        {
                            var address = signer.EncodeUTF8AndEcRecover(message, sig.CreateStringSignature());
                            bool ok = addr.ToLower() == address.ToLower();
                            if (ok)
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
                catch
                {
                    return false;
                }
            }
            return false;
        }
        public static bool DoSimpleDeposit(string ethTxId, out Transaction tx)
        {
            tx = default;
            OTCExchangeRequest request = new OTCExchangeRequest
            {
                EthTxHash = UInt256.Parse(ethTxId)
            };

            var at = BuildAskTransaction(Mining.MasterAccountAddress, (byte)InvestType.OTCExchangeRequest, request.ToArray());
            if (at.IsNotNull())
            {
                tx = at.BuildAndSignNoneOutput(Fixed8.Zero);
                return true;
            }
            return false;
        }
        public static AskTransaction BuildAskTransaction(UInt160 bizScriptHash, byte DataType, byte[] data, uint maxindex = 0x00, uint minindex = 0x00, byte edgeVersion = 0x00)
        {
            if (!Blockchain.Singleton.VerifySlotValidator(bizScriptHash, out AccountState _, out Fixed8 balance, out Fixed8 askFee)) return default;
            AskTransaction ct = new AskTransaction();
            ct.EdgeVersion = edgeVersion;
            ct.DataType = DataType;
            ct.Data = data;
            ct.From = SecureHelper.MasterAccount.Key.PublicKey;
            ct.BizScriptHash = bizScriptHash;
            ct.MaxIndex = maxindex;
            ct.MinIndex = minindex;
            if (askFee > Fixed8.Zero)
            {
                List<TransactionOutput> list = new List<TransactionOutput>();
                if (ct.Outputs.IsNotNullAndEmpty())
                    list.AddRange(ct.Outputs);
                list.Add(new TransactionOutput() { AssetId = Blockchain.OXC, ScriptHash = bizScriptHash, Value = askFee });
                ct.Outputs = list.ToArray();
            }
            return ct;
        }
    }
}
