using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.WebPort
{
    public static class SwapHelper
    {
        public static UInt160 BuildAssetPoolAddress(UInt256 assetId)
        {
            Contract contract = default;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitPush(Mining.SidePoolAccountPubKey);
                sb.EmitPush((byte)0);
                sb.EmitPush(assetId.ToArray());
                sb.EmitPush((byte)SideType.AssetID);
                sb.EmitPush((byte)0);
                sb.EmitAppCall(Blockchain.SideAssetContractScriptHash);
                contract = Contract.Create(new[] { ContractParameterType.Signature }, sb.ToArray());
            } 
            return contract.ScriptHash;
        }
    }
}
