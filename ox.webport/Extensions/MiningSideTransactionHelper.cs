﻿using OX.Ledger;
using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.IO;

namespace OX.WebPort
{
    public static class MiningSideTransactionHelper
    {
        public static readonly Fixed8 MinSidePoolOXC = Fixed8.One * 1000;
        public static bool VerifyRegMainSwap(this SlotSideTransaction tx, out UInt256 Asset,out SwapPairReply swapPairReply)
        {
            Asset = default;
            swapPairReply = default;
            if (!tx.Slot.Equals(Mining.SidePoolAccountPubKey) || tx.Flag != 0 || tx.SideType != SideType.AssetID || !tx.AuthContract.Equals(Blockchain.SideAssetContractScriptHash)) return false;
            if (!tx.GetPublicKeys().Contains(Mining.MasterAccountPubKey)) return false;
            if (tx.Attach.IsNullOrEmpty()) return false;
            try
            {
                Asset = tx.Data.AsSerializable<UInt256>();
                swapPairReply = tx.Attach.AsSerializable<SwapPairReply>();
                if (swapPairReply.TargetAssetId != Asset) return false;                
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool VerifyRegSideSwap(this SlotSideTransaction tx, out UInt256 Asset)
        {
            Asset = default;
            if (!tx.Slot.Equals(Mining.SlaveSidePoolAccountPubKey) || tx.Flag != 1 || tx.SideType != SideType.AssetID || !tx.AuthContract.Equals(Blockchain.SideAssetContractScriptHash)) return false;
            try
            {
                var assetId = tx.Data.AsSerializable<UInt256>();
                Asset = assetId;
                var sh = tx.GetContract().ScriptHash;
                var outputs = tx.Outputs.Where(m => m.AssetId.Equals(Blockchain.OXC) && m.ScriptHash.Equals(sh));
                if (outputs.IsNullOrEmpty()) return false;
                if (outputs.Sum(m => m.Value) < MinSidePoolOXC) return false;
                outputs = tx.Outputs.Where(m => m.AssetId.Equals(assetId) && m.ScriptHash.Equals(sh));
                if (outputs.IsNullOrEmpty()) return false;
                if (outputs.Sum(m => m.Value) < MinSidePoolOXC) return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool VerifyRegSideSwapFee(this SlotSideTransaction tx, Fixed8 SidePoolFeeSetting)
        {
            var outputs = tx.Outputs.Where(m => m.AssetId.Equals(Blockchain.OXC) && m.ScriptHash.Equals(Mining.SlaveSidePoolAccountAddress));
            if (outputs.IsNullOrEmpty()) return false;
            if (outputs.Sum(m => m.Value) < SidePoolFeeSetting) return false;
            return true;
        }
    }
}
