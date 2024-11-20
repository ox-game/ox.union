using OX.IO;
using OX.IO.Data.LevelDB;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX;
using OX.VM;
using OX.Ledger;
using System.Linq;
using System;
using System.Runtime.CompilerServices;
using OX.BMS;
using System.Net.Http.Headers;
using OX.Casino;

namespace OX.WebPort
{
    public static partial class CasinoPersistenceHelper
    {
        public static void Save_AnchorMortgageIssueRecord(this WriteBatch batch, MiningBlockIndex provider, Block block, EthereumMapTransaction emt, TransactionOutput output)
        {
            AnchorMortgageIssueKey key = new AnchorMortgageIssueKey
            {
                EthereumAddress = emt.EthereumAddress.ToLower(),
                TxHash = emt.Hash,
                Timestamp = block.Timestamp
            };
            batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.AMI_USDTCastRecord).Add(key), SliceBuilder.Begin().Add(output));
        }
        public static void Save_AnchorMortgageDestroyRecord(this WriteBatch batch, MiningBlockIndex provider, Block block, Transaction tx, TransactionOutput output, string ethAddress)
        {
            AnchorMortgageIssueKey key = new AnchorMortgageIssueKey
            {
                EthereumAddress = ethAddress.ToLower(),
                TxHash = tx.Hash,
                Timestamp = block.Timestamp
            };
            batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.AMI_USDTDestroyRecord).Add(key), SliceBuilder.Begin().Add(output));
        }
        public static void Save_InvestSettingRecord(this WriteBatch batch, BizRecordModel model, InvestSettingRecord record)
        {
            if (record != default && record.Value != default)
                batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.Invest_Setting).Add(model.Key), SliceBuilder.Begin().Add(record));
        }
        public static void Save_MainSwapPair(this WriteBatch batch, MiningBlockIndex provider, Block block, SlotSideTransaction st, SwapPairReply reply)
        {
            if (reply.IsNotNull())
            {
                var sh = st.GetContract().ScriptHash;
                var TargetAssetState = Blockchain.Singleton.Store.GetAssets().TryGet(reply.TargetAssetId);
                var merge = new SwapPairMerge { PoolAddress = sh, TxId = st.Hash, SwapPairReply = reply, TargetAssetState = TargetAssetState, Index = block.Index };
                if (reply.Mark.IsNotNullAndEmpty())
                {
                    if (reply.Mark.TryAsSerializable<SwapPairIDO>(out var ido))
                    {
                        merge.IDO = ido;
                    }
                }
                batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.Exchange_Pair).Add(sh), SliceBuilder.Begin().Add(merge));
                provider.SwapPairs[sh] = merge;
            }
        }
        public static void Save_SwapPairStateReply(this WriteBatch batch, MiningBlockIndex miningProvider, Block block, ReplyTransaction rt, SwapPairStateReply reply)
        {
            if (reply.IsNotNull())
            {
                var hostSH = reply.PoolAddress;
                batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.Exchange_Pair_State).Add(hostSH), SliceBuilder.Begin().Add(reply));
                miningProvider.SwapPairStates[hostSH] = reply;
            }
        }
        public static void Save_SwapPairExchange(this WriteBatch batch, MiningBlockIndex miningProvider, Block block, ushort TxN, Transaction tx)
        {
            if (tx.References.IsNotNullAndEmpty())
            {
                foreach (var reference in tx.References)
                {
                    if (miningProvider.SwapPairs.TryGetValue(reference.Value.ScriptHash, out SwapPairMerge spm))
                    {
                        if (spm.TargetAssetState.AssetId.Equals(reference.Value.AssetId) || Blockchain.OXC.Equals(reference.Value.AssetId))
                        {
                            var attr = tx.Attributes.FirstOrDefault(m => m.Usage == TransactionAttributeUsage.Remark2);
                            if (attr.IsNotNull())
                            {
                                try
                                {
                                    var sop = attr.Data.AsSerializable<SwapVolume>();
                                    if (sop.IsNotNull())
                                    {
                                        var vom = new SwapVolumeMerge { Volume = sop, Price = (decimal)sop.PricingAssetVolume.GetInternalValue() / (decimal)sop.TargetAssetVolume.GetInternalValue() };
                                        long sortIndex = 0;
                                        if (miningProvider.LastSwapVolume.TryGetValue(reference.Value.ScriptHash, out var swapvolumemerge))
                                        {
                                            sortIndex = (long)swapvolumemerge.Volume.BlockIndex * 10000 + (long)swapvolumemerge.Volume.TxN;
                                        }
                                        long newsortIndex = (long)sop.BlockIndex * 10000 + (long)sop.TxN;
                                        if (newsortIndex > sortIndex)
                                        {
                                            batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.Exchange_Pair_Record_Last).Add(reference.Value.ScriptHash), SliceBuilder.Begin().Add(vom));
                                            miningProvider.LastSwapVolume[reference.Value.ScriptHash] = vom;
                                        }
                                    }
                                }
                                catch { }
                            }
                            //var attr3 = tx.Attributes.FirstOrDefault(m => m.Usage == TransactionAttributeUsage.Remark3);
                            //if (attr3.IsNotNull())
                            //{
                            //    try
                            //    {
                            //        var idor = attr3.Data.AsSerializable<IDORecord>();
                            //        if (idor.IsNotNull())
                            //        {
                            //            SwapIDOKey key = new SwapIDOKey { PoolAddress = idor.PoolAddress, IDOOwner = idor.IdoOwner, TxId = tx.Hash };
                            //            batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.Exchange_IDO_Record).Add(key), SliceBuilder.Begin().Add(idor));
                            //        }
                            //    }
                            //    catch { }
                            //}

                            break;
                        }
                    }
                }
            }

        }
        public static void Save_OTCDealer(this WriteBatch batch, MiningBlockIndex miningProvider, Block block, SlotSideTransaction st, string ethAddress, OTCSetting setting)
        {
            var sh = st.GetContract().ScriptHash;
            if (miningProvider.OTCDealers.TryGetValue(sh, out OTCDealerMerge dealerMerge))
            {
                dealerMerge.Setting = setting;
            }
            else
            {
                dealerMerge = new OTCDealerMerge { EthAddress = ethAddress, InPoolAddress = sh, Setting = setting };
                miningProvider.OTCDealers[sh] = dealerMerge;
            }
            batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.OTC_Dealer).Add(sh), SliceBuilder.Begin().Add(dealerMerge));
        }
    }
}
