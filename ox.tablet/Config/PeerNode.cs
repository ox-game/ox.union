using OX.IO.Json;

namespace OX.Tablet.Config
{
    public class PeerNode
    {
        public string IP;
        public string Port;
        public PeerNode(string ip, string port)
        {
            this.IP = ip;
            this.Port = port;
        }
        public override bool Equals(object obj)
        {
            if (obj is PeerNode pn)
            {
                return pn.IP == this.IP && pn.Port == this.Port;
            }
            return base.Equals(obj);
        }
        public override string ToString()
        {
            return $"{IP}:{Port}";
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public static PeerNode FromJson(JObject json)
        {
            return new PeerNode(json["ip"].AsString(), json["port"]?.AsString());
        }
        public JObject ToJson()
        {
            JObject account = new JObject();
            account["ip"] = this.IP;
            account["port"] = this.Port;
            return account;
        }
    }
}
