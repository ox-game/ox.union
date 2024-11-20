using OX.IO.Json;

namespace OX.Tablet.Config
{
    public class AgentFeeNode
    {
        public string MemberId;
        public string Rate;
        public string Nick;
        public AgentFeeNode(string memberid, string rate, string nick)
        {
            this.MemberId = memberid;
            this.Rate = rate;
            Nick = nick;
        }
        public override bool Equals(object obj)
        {
            if (obj is AgentFeeNode pn)
            {
                return pn.MemberId == this.MemberId;
            }
            return base.Equals(obj);
        }
        public override string ToString()
        {
            return $"{MemberId}:{Rate}";
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public static AgentFeeNode FromJson(JObject json)
        {
            return new AgentFeeNode(json["memberid"].AsString(), json["rate"]?.AsString(), json["nick"]?.AsString());
        }
        public JObject ToJson()
        {
            JObject account = new JObject();
            account["memberid"] = this.MemberId;
            account["rate"] = this.Rate;
            account["nick"] = this.Nick;
            return account;
        }
    }
}
