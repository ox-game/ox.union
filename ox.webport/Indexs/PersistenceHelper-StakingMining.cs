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
using Akka.IO;

namespace OX.WebPort
{
    public static partial class CasinoPersistenceHelper
    {
        public static void Save_LockMiningAssetReply(this WriteBatch batch, MiningBlockIndex miningProvider, Block block, ReplyTransaction rt, LockMiningAssetReply reply)
        {
            if (reply.IsNotNull())
            {
                batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.LockMiningAssetReply).Add(new LockMiningAssetKey { AssetId = reply.AssetId, IssueIndex = block.Index }), SliceBuilder.Begin().Add(reply));
            }
        }
     
        public static void Save_MutualLockNodeForEth(this WriteBatch batch, MiningBlockIndex miningProvider, Block block, Transaction tx, TransactionOutput output, MutualNode parentNode, string ethAddress)
        {
            var from = ethAddress.BuildMapAddress();
            var seedSH = from.GetMutualLockSeed();
            if (!miningProvider.MutualLockNodes.ContainsKey(seedSH))
            {
                byte nodeType = (byte)(output.ScriptHash.Equals(MutualLockHelper.GenesisSeed()) ? 1 : 0);
                var node = new MutualNode { HolderAddress = from, RegIndex = block.Index, NodeType = nodeType, RootSeedAddress = parentNode.IsNotNull() ? parentNode.RootSeedAddress : seedSH, ParentHolder = parentNode.IsNotNull() ? parentNode.HolderAddress : Mining.MasterAccountAddress, IsEthMap = true };
                batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.MutualLockNode).Add(seedSH), SliceBuilder.Begin().Add(node));
                miningProvider.MutualLockNodes[seedSH] = node;

            }
        }
        public static void Save_MutualLockNode(this WriteBatch batch, MiningBlockIndex miningProvider, Block block, Transaction tx, TransactionOutput output, MutualNode parentNode)
        {
            var fromPubKey = tx.GetBestWitnessPublicKey();
            if (fromPubKey.IsNotNull())
            {
                var from = Contract.CreateSignatureRedeemScript(fromPubKey).ToScriptHash();
                var seedSH = from.GetMutualLockSeed();
                if (!miningProvider.MutualLockNodes.ContainsKey(seedSH))
                {
                    byte nodeType = (byte)(output.ScriptHash.Equals(MutualLockHelper.GenesisSeed()) ? 1 : 0);
                    var node = new MutualNode { HolderAddress = from, RegIndex = block.Index, NodeType = nodeType, RootSeedAddress = parentNode.IsNotNull() ? parentNode.RootSeedAddress : seedSH, ParentHolder = parentNode.IsNotNull() ? parentNode.HolderAddress : Mining.MasterAccountAddress, IsEthMap = false };
                    batch.Put(SliceBuilder.Begin(MiningPersistencePrefixes.MutualLockNode).Add(seedSH), SliceBuilder.Begin().Add(node));
                    miningProvider.MutualLockNodes[seedSH] = node;
                }
            }
        }
      
    }
}
