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

namespace OX.WebPort.Config
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
        const string basepath = "c:\\oxwebport\\";
        const string path = basepath + "node.json";
        protected JObject Node;

        public List<PeerNode> ExtSeedPeers = new List<PeerNode>();
        public List<PeerNode> CollectPeers = new List<PeerNode>();

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

        }


        public virtual void Save()
        {
            Node = new JObject();

            Node["extseedpeers"] = new JArray(ExtSeedPeers.Select(p => p.ToJson()));
            Node["collectpeers"] = new JArray(CollectPeers.Select(p => p.ToJson()));
            File.WriteAllText(path, Node.ToString());
        }

    }
}
