using Akka.Util;
using OX.Bapps;
using OX.IO;
using OX.IO.Data.LevelDB;
using OX.IO.Wrappers;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using OX.Tablet.Models;
using OX.Cryptography.ECC;
using OX.BMS;
using OX.Casino;
using Akka.Actor;
using OX.SmartContract;
using Akka.IO;
using Nethereum.Model;

namespace OX.Tablet
{
    public class MiningBlockIndex : BaseSubBlockIndex
    {
        public const string IndexKey = "Data_MiningIndex";
        public override bool CanRebuild => true;

        public Dictionary<UInt160, SwapPairMerge> SwapPairs { get; set; } = new Dictionary<UInt160, SwapPairMerge>();
        public Dictionary<UInt160, SwapPairStateReply> SwapPairStates { get; set; } = new Dictionary<UInt160, SwapPairStateReply>();
        public Dictionary<UInt160, OTCDealerMerge> OTCDealers { get; set; } = new Dictionary<UInt160, OTCDealerMerge>();
        /// <summary>
        /// key is node seed address,value is node private address
        /// </summary>
        public Dictionary<UInt160, MutualNode> MutualLockNodes { get; set; } = new Dictionary<UInt160, MutualNode>();
        public MiningBlockIndex(BlockIndex blockIndex, OXAccount oxAccount, EthAccount ethAccount) : base(blockIndex, oxAccount, ethAccount, IndexKey)
        {
            SwapPairs = new Dictionary<UInt160, SwapPairMerge>(this.GetAll<UInt160, SwapPairMerge>(MiningPersistencePrefixes.Exchange_Pair));
            SwapPairStates = new Dictionary<UInt160, SwapPairStateReply>(this.GetAll<UInt160, SwapPairStateReply>(MiningPersistencePrefixes.Exchange_Pair_State));
            OTCDealers = new Dictionary<UInt160, OTCDealerMerge>(this.GetAll<UInt160, OTCDealerMerge>(MiningPersistencePrefixes.OTC_Dealer));
            MutualLockNodes = new Dictionary<UInt160, MutualNode>(this.GetAll<UInt160, MutualNode>(MiningPersistencePrefixes.MutualLockNode));
        }

        public override void Rebuild()
        {
            this.SwapPairs.Clear();
            this.SwapPairStates.Clear();
            this.OTCDealers.Clear();
            this.MutualLockNodes.Clear();
            base.Rebuild();
        }
        public override void HandleBlock(Block block)
        {
            WriteBatch batch = new WriteBatch();
            BlockContext context = new BlockContext();
            for (ushort i = 0; i < block.Transactions.Length; i++)
            {
                var tx = block.Transactions[i];
                if (this.IsBizTransaction(tx, Mining.MasterAccountAddress, out BizTransaction biztx))
                {
                    ushort? n = null;
                    if (biztx is BillTransaction bt)
                    {
                        foreach (var record in bt.Records)
                        {
                            var bizModel = InvestBizRecordHelper.BuildModel(record);
                            if (bizModel.Model is InvestSettingRecord InvestSettingRecord) batch.Save_InvestSettingRecord(bizModel, InvestSettingRecord);
                        }
                    }
                    else if (biztx is ReplyTransaction rt)
                    {
                        OnMiningReplayTransaction(batch, block, rt);
                    }
                    else if (biztx is AskTransaction at)
                    {
                    }
                }
                else if (tx is LockAssetTransaction lat)
                {
                }
                else if (tx is IssueTransaction ist)
                {
                }
                else if (tx is EthereumMapTransaction emt)
                {
                    OnEthereumMapTransaction(batch, block, emt, i);
                }
                else if (tx is SlotSideTransaction st)
                {
                    OnSideTransaction(batch, block, st);
                }
                else if (tx is RangeTransaction rgt)
                {
                }
                if (tx.Outputs.IsNotNullAndEmpty())
                {
                    for (ushort k = 0; k < tx.Outputs.Length; k++)
                    {
                        TransactionOutput output = tx.Outputs[k];
                        OnPledgeMiningTransaction(batch, this, block, tx, output, k);
                        CheckUSDTBlackHoleDestroy(batch, block, output, tx);
                    }
                }
                batch.Save_SwapPairExchange(this, block, i, tx);
            }
            this.Db.Write(WriteOptions.Default, batch);
        }
        public void CheckUSDTBlackHoleDestroy(WriteBatch batch, Block block, TransactionOutput output, Transaction tx)
        {
            if (output.ScriptHash.Equals(UInt160.Zero) && output.AssetId.Equals(Mining.USDT_Asset))
            {
                if (tx.EthSignatures.IsNotNullAndEmpty() && tx.EthSignatures.Count() <= 2)
                {
                    foreach (var sig in tx.EthSignatures)
                    {
                        try
                        {
                            var stringToSign = tx.InputOutputHash.ToArray().ToHexString();
                            var signer = new Nethereum.Signer.EthereumMessageSigner();
                            var ethAddr = signer.EncodeUTF8AndEcRecover(stringToSign, sig.CreateStringSignature());
                            if (ethAddr.IsNotNullAndEmpty() && ethAddr.ToLower() == SecureHelper.ExchangeAccount.EthAddress.ToLower())
                            {
                                batch.Save_AnchorMortgageDestroyRecord(this, block, tx, output, ethAddr);
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        public void OnEthereumMapTransaction(WriteBatch batch, Block block, EthereumMapTransaction emt, ushort TxN)
        {
            if (emt.EthMapContract.Equals(Blockchain.EthereumMapContractScriptHash))
            {
                OnEthereumMapTransactionForAnchorMortgageIssue(batch, block, emt, TxN);
            }
        }
        public void OnEthereumMapTransactionForAnchorMortgageIssue(WriteBatch batch, Block block, EthereumMapTransaction emt, ushort TxN)
        {
            if (emt.VerifyAnchorMortgageIssue(out TransactionOutput output))
            {
                batch.Save_AnchorMortgageIssueRecord(this, block, emt, output);
            }
        }
        public void OnSideTransaction(WriteBatch batch, Block block, SlotSideTransaction st)
        {
            UInt256 AssetId = default;
            if (st.VerifyRegMainSwap(out AssetId, out SwapPairReply swapPairReply))
            {
                batch.Save_MainSwapPair(this, block, st, swapPairReply);
            }
            else if (st.VerifyRegSideSwap(out AssetId))
            {
                //var settings = this.GetAllInvestSettings();
                //var feesetting = settings.FirstOrDefault(m => Enumerable.SequenceEqual(m.Key, new[] { InvestSettingTypes.SidePairRegFee }));
                //if (feesetting.Equals(new KeyValuePair<byte[], InvestSettingRecord>())) return;
                //var fee = Fixed8.FromDecimal(decimal.Parse(feesetting.Value.Value));
                //if (st.VerifyRegSideSwapFee(fee))
                //{
                //    batch.Save_SideSwapPair(this, block, st, AssetId);
                //}
            }
            else if (st.VerifyOTCDealerTx(out string ethAddress, out OTCSetting setting))
            {
                batch.Save_OTCDealer(this, block, st, ethAddress, setting);
            }
        }
        public void OnMiningReplayTransaction(WriteBatch batch, Block block, ReplyTransaction rt)
        {
            var bizshs = new UInt160[] { Mining.MasterAccountAddress };
            IReadOnlyDictionary<CoinReference, TransactionOutput> rfs = rt.References;
            switch (rt.DataType)
            {
                case (byte)InvestType.SwapPairStateReply:
                    if (rt.GetDataModel<SwapPairStateReply>(bizshs, (byte)InvestType.SwapPairStateReply, out SwapPairStateReply SwapPairStateReply))
                    {
                        batch.Save_SwapPairStateReply(this, block, rt, SwapPairStateReply);
                    }
                    break;
                case (byte)InvestType.LockMiningAssetReply:
                    if (rt.GetDataModel<LockMiningAssetReply>(bizshs, (byte)InvestType.LockMiningAssetReply, out LockMiningAssetReply LockMiningAssetReply))
                    {
                        batch.Save_LockMiningAssetReply(this, block, rt, LockMiningAssetReply);
                    }
                    break;
            }

        }
        public IEnumerable<KeyValuePair<byte[], InvestSettingRecord>> GetAllInvestSettings()
        {
            return this.Db.Find(ReadOptions.Default, SliceBuilder.Begin(MiningPersistencePrefixes.Invest_Setting), (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<byte[], InvestSettingRecord>(ks, data.AsSerializable<InvestSettingRecord>());
            });
        }
        public string GetAnchorMortgageIssuePoolAddress()
        {
            var setting = this.GetAllInvestSettings().FirstOrDefault(m => Enumerable.SequenceEqual(m.Key, new[] { InvestSettingTypes.AnchorIssuePool }));
            if (setting.Equals(new KeyValuePair<byte[], InvestSettingRecord>()))
                return string.Empty;
            return setting.Value.Value;
        }


        public void OnPledgeMiningTransaction(WriteBatch batch, MiningBlockIndex miningProvider, Block block, Transaction tx, TransactionOutput output, ushort k)
        {
            MutualNode parentNode = default;
            var isRootReg = output.ScriptHash.Equals(MutualLockHelper.GenesisSeed());
            var isCommonReg = this.MutualLockNodes.TryGetValue(output.ScriptHash, out parentNode);
            if (isRootReg || isCommonReg)
            {

                //reg miner
                if (output.VerifyMutualLockNodeRegister())
                {
                    if (tx.IsOnlyFromEthereumMapAddress(out string ethAddress))
                    {
                        batch.Save_MutualLockNodeForEth(this, block, tx, output, parentNode, ethAddress);
                    }
                    else
                        batch.Save_MutualLockNode(this, block, tx, output, parentNode);
                }
            }
        }

    }
}
