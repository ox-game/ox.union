using Akka.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using Org.BouncyCastle.Asn1.X509;
using OX.Bapps;
using OX.BMS;
using OX.Mix.ApiModels;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OX.WebPort
{
    public class MarkMemberApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "member";
        public override ApiAction Build()
        {
            return new MarkMemberApi();
        }
    }
    public class MarkMemberApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            if (uint.TryParse(arg, out var memberId))
            {
                var markIndex = BlockIndex.Instance.GetSubBlockIndex<MarkBlockIndex>();
                var member = markIndex.AllMarkMembers.FirstOrDefault(m => m.MarkMemberId == memberId);
                if (member.IsNull()) return controller.StatusCode(500);

                ApiMarkMemberModel ammm = new ApiMarkMemberModel
                {
                    MarkMemberId = member.MarkMemberId.ToString(),
                    MarkMemberHolder = member.Holder.ToAddress(),
                    MarkMemberPubKey = member.HolderPubkey.ToString(),
                    ExpireTimeStamp = member.ExpireTimeStamp.ToString(),
                    TotalBetAmount = member.TotalBetAmount.ToString(),
                    TotalPrizeAmount = member.TotalPrizeAmount.ToString(),
                    MemberType = member.Request.MemberType.Value().ToString()
                };
                if (markIndex.MarkMembers.TryGetValue(member.Request.PortHolder, out var pm))
                {
                    ammm.PortMemberId = pm.MarkMemberId.ToString();
                    ammm.PortHolder = pm.Holder.ToAddress();
                    ammm.PortHolderPubKey = pm.HolderPubkey.ToString();
                }
                var json = JsonConvert.SerializeObject(ammm);
                return controller.Content(json);
            }
            return controller.StatusCode(500);
        }
        public override IActionResult ActionPost(ControllerBase controller, string arg, string flag)
        {
            return controller.StatusCode(500);
        }
    }

}
