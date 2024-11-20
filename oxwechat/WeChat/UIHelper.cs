using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OX.WeChat
{
    //string wifiConfig = $"WIFI:T/{ssid};P/{password};;";
    public interface ILanguage
    {
        public string Language { get; }
    }
    public static class UIHelper
    {

        public static bool IsChina()
        {
            return System.Globalization.CultureInfo.InstalledUICulture.Name.ToLower() == "zh-cn";
        }
        public static string LocalString(string ChinaString, string EnglishString)
        {
            return IsChina() ? ChinaString : EnglishString;
        }
        public static string WebLocalString(this ILanguage language, string ChinaString, string EnglishString)
        {
            return WebLocalString(language.Language, ChinaString, EnglishString);
        }
        public static string WebLocalString(string language, string ChinaString, string EnglishString)
        {
            if (language.IsNotNullAndEmpty())
            {
                return language.ToLower() == "zh-cn" ? ChinaString : EnglishString;
            }
            else
            {
                return IsChina() ? ChinaString : EnglishString;
            }
        }
    }
}
