using OX;
using OX.IO.Json;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets.NEP6;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UserWallet = OX.Wallets.SQLite.UserWallet;

namespace OX.Tablet.Config
{
    public class NodeConfig
    {
        static NodeConfig _instance;
        public static NodeConfig Instance
        {
            get
            {
                if (_instance.IsNull())
                    _instance = new NodeConfig();
                return _instance;
            }
        }
        const string basepath = "c:\\oxtablet\\";
        const string path = basepath + "nodeaccount.json";
     
        protected JObject Node;
        public string CipherPriKey = string.Empty;
        public string RunMode = string.Empty;
        public string Contacts = string.Empty;
        public string Email = string.Empty;
        public string MarkAutoCut = string.Empty;
        public List<PeerNode> ExtSeedPeers = new List<PeerNode>();
        public List<PeerNode> CollectPeers = new List<PeerNode>();
        public List<AgentFeeNode> AgentFees = new List<AgentFeeNode>();
        NodeConfig()
        {
            if (!System.IO.Directory.Exists(basepath))
                System.IO.Directory.CreateDirectory(basepath);
            if (!File.Exists(path))
            {
                Save();
            }
            Init();        
        }
        void Init()
        {
            using (StreamReader reader = new StreamReader(path))
            {
                Node = JObject.Parse(reader);
            }
            this.CipherPriKey = Node["cipherprikey"]?.AsString();
            this.RunMode = Node["runmode"]?.AsString();
            this.Contacts = Node["contacts"]?.AsString();
            this.Email = Node["email"]?.AsString();
            this.MarkAutoCut = Node["markautocut"]?.AsString();
            var jExtSeedPeers = Node["extseedpeers"];
            if (jExtSeedPeers != default)
            {
                this.ExtSeedPeers = ((JArray)jExtSeedPeers).Select(p => PeerNode.FromJson(p)).ToList();
            }
            var jCollectPeers = Node["collectpeers"];
            if (jCollectPeers != default)
            {
                this.CollectPeers = ((JArray)jCollectPeers).Select(p => PeerNode.FromJson(p)).ToList();
            }
            var jAgentFees = Node["agentfees"];
            if (jAgentFees != default)
            {
                this.AgentFees = ((JArray)jAgentFees).Select(p => AgentFeeNode.FromJson(p)).ToList();
            }
        }

        public bool AutoCutMarkBet => this.MarkAutoCut?.ToLower() == "true";

        public virtual void Save()
        {
            Node = new JObject();
            Node["cipherprikey"] = this.CipherPriKey;
            Node["runmode"] = this.RunMode;
            Node["contacts"] = this.Contacts;
            Node["email"] = this.Email;
            Node["markautocut"] = this.MarkAutoCut;
            Node["extseedpeers"] = new JArray(ExtSeedPeers.Select(p => p.ToJson()));
            Node["collectpeers"] = new JArray(CollectPeers.Select(p => p.ToJson()));
            Node["agentfees"] = new JArray(AgentFees.Select(p => p.ToJson()));
            File.WriteAllText(path, Node.ToString());
        }

    }
}
