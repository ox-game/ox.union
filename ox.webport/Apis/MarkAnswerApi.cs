using Akka.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using OX.Bapps;
using OX.BMS;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OX.WebPort
{
    public class MarkAnswerApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "getanswer";
        public override ApiAction Build()
        {
            return new MarkAnswerApi();
        }
    }
    public class MarkAnswerApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            var casinoIndex =BlockIndex.Instance.GetSubBlockIndex<CasinoBlockIndex>();
            if (casinoIndex.IsNotNull())
            {
                List<GuessAnswer> answers = new List<GuessAnswer>();
                if (arg.IsNotNullAndEmpty())
                {
                    if (arg == "all")
                    {
                        List<MarkAnswer> mas = new List<MarkAnswer>();
                        foreach (var aw in casinoIndex.GuessAnswers.OrderByDescending(m => m.Value.Key.Term.ToDateTime()))
                        {
                            var round = (MarkSixRound)aw.Value.Key.ChannelRound.Round;
                            var name = round.GetName().Name;
                            mas.Add(new MarkAnswer
                            {
                                Term = aw.Value.Key.Term.ToString(),
                                Result = aw.Value.Value.ToString(),
                                Name = name
                            });
                        }
                        var alljson = JsonConvert.SerializeObject(mas);
                        return controller.Content(alljson);
                    }
                    else if (DateTime.TryParse(arg, out var date))
                    {
                        foreach (var round in NoneFlagEnumHelper.All<MarkSixRound>())
                        {
                            MarkChannelRound cr = new MarkChannelRound { Channel = BetChannel.MarkSix, Round = (byte)round };
                            GuessAnswerKey key = new GuessAnswerKey
                            {
                                ChannelRound = cr,
                                Term = new MarkTerm((ushort)date.Year, (byte)date.Month, (byte)date.Day)
                            };
                            if (casinoIndex.GuessAnswers.TryGetValue(key.ToString(), out var ga))
                            {
                                answers.Add(ga);
                            }
                        }
                    }
                    else
                        return controller.StatusCode(500);
                }
                else
                {
                    answers = casinoIndex.LatestGuessAnswer.Values.ToList();
                }

                List<MarkAnswer> list = new List<MarkAnswer>();
                foreach (var answer in answers)
                {
                    var round = (MarkSixRound)answer.Key.ChannelRound.Round;
                    var name = round.GetName().Name;
                    list.Add(new MarkAnswer
                    {
                        Term = answer.Key.Term.ToString(),
                        Result = answer.Value.ToString(),
                        Name = name
                    });
                }
                var json = JsonConvert.SerializeObject(list);
                return controller.Content(json);
            }
            return controller.StatusCode(500);
        }
        public override IActionResult ActionPost(ControllerBase controller, string arg, string flag)
        {
            return controller.StatusCode(500);
        }
    }

    public class MarkAnswer
    {
        public string Term;
        public string Name;
        public string Result;
    }
}
