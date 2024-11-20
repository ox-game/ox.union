using OX.Network.P2P.Payloads;
using System;
using OX.SmartContract;
using OX.Wallets;

namespace OX.Tablet.FlashMessages
{
    public class UnicastChatboxInfo
    {
        public Tuple<UInt256, UnicastTalkLineValue> TP { get; set; }
        public OXAccount LocalAccount { get; set; }
       
        public string ChatPlaceholder = "Please enter a message...";
        public byte[] Attachment { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentType { get; set; }
        public string GetLabel()
        {
            var label = TP.Item2.Label;
            if (label.IsNullOrEmpty())
            {
                label = Contract.CreateSignatureRedeemScript(TP.Item2.Remote).ToScriptHash().ToAddress();
            }
            return label;
        }
    }
}
