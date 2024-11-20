using OX.Cryptography.ECC;
using OX.IO.Json;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using OX.SmartContract;
using OX.Wallets;
using OX.Wallets.NEP6;
using OX.BMS;
using OX.Tablet.UIs.MarkSix;
using OX.Tablet.UIs.MarkOne;
using OX.Consensus;
using OX.Tablet.UIs.Mark.BetForms;

namespace OX.Tablet.UIs.Mark
{
    public interface IBetFormBuilder
    {
        public BaseBetForm Build();

    }
    public class BetFormBuilder<T> : IBetFormBuilder where T : BaseBetForm, new()
    {
        public BaseBetForm Build()
        {
            return new T();
        }
    }
    public static class BetUIHelper
    {
        public static bool AllowOrderSwitchCheck = false;
        public static Dictionary<MarkSixBetMethod, IBetFormBuilder> FullBetForms = new Dictionary<MarkSixBetMethod, IBetFormBuilder>();
        public static Dictionary<MarkOneBetMethod, IBetFormBuilder> SimpleBetForms = new Dictionary<MarkOneBetMethod, IBetFormBuilder>();
        static BetUIHelper()
        {
            FullBetForms[MarkSixBetMethod.TM] = new BetFormBuilder<SpecialCodeBetForm>();
            FullBetForms[MarkSixBetMethod.ZM2] = new BetFormBuilder<ZM2CodeBetForm>();
            //FullBetForms[FullMarkSixBetMethod.ZM3] = new BetFormBuilder<ZM3CodeBetForm>();
            FullBetForms[MarkSixBetMethod.TP] = new BetFormBuilder<TPCodeBetForm>();
            FullBetForms[MarkSixBetMethod.PT1X] = new BetFormBuilder<PT1XBetForm>();
            FullBetForms[MarkSixBetMethod.PT2X] = new BetFormBuilder<PT2XBetForm>();
            FullBetForms[MarkSixBetMethod.PT3X] = new BetFormBuilder<PT3XBetForm>();
            FullBetForms[MarkSixBetMethod.PT4X] = new BetFormBuilder<PT4XBetForm>();
            FullBetForms[MarkSixBetMethod.PT5X] = new BetFormBuilder<PT5XBetForm>();
            FullBetForms[MarkSixBetMethod.TM_Color] = new BetFormBuilder<TMColorBetForm>();
            FullBetForms[MarkSixBetMethod.DXDSJY] = new BetFormBuilder<DualisticFullBetForm>();
            FullBetForms[MarkSixBetMethod.Color_DS] = new BetFormBuilder<ColorDSBetForm>();
            FullBetForms[MarkSixBetMethod.PTTail] = new BetFormBuilder<TailBetForm>();

            SimpleBetForms[MarkOneBetMethod.Zodiac] = new BetFormBuilder<SpecialZodiacBetForm>();
            SimpleBetForms[MarkOneBetMethod.ZodiacColor] = new BetFormBuilder<ZodiacColorBetForm>();
            SimpleBetForms[MarkOneBetMethod.JYYYTD] = new BetFormBuilder<DualisticSimpleBetForm>();
        }
        public static BaseBetForm GetBetForm(this MarkSixBetMethod method)
        {
            IBetFormBuilder builder = default;
            FullBetForms.TryGetValue(method, out builder);
            if (builder.IsNotNull())
                return builder.Build();
            return default;
        }
        public static BaseBetForm GetBetForm(this MarkOneBetMethod method)
        {
            IBetFormBuilder builder = default;
            SimpleBetForms.TryGetValue(method, out builder);
            if (builder.IsNotNull())
                return builder.Build();
            return default;
        }
        public static ulong GetNonce()
        {
            byte[] nonce = new byte[sizeof(ulong)];
            Random rand = new Random();
            rand.NextBytes(nonce);
            return nonce.ToUInt64(0);
        }
    }
}
