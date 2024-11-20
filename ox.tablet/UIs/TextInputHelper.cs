using OX.BMS;
using OX.Cryptography;
using OX.DirectSales;
using OX.IO;
using OX.Tablet.Config;
using OX.Tablet.UIs.Mark;
using System;
using System.IO;

namespace OX.Tablet.UIs
{
    public class TextInputArray : ISerializable
    {
        public TextInput[] Inputs;
        public virtual int Size => Inputs.GetVarSize();


        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Inputs);
        }
        public void Deserialize(BinaryReader reader)
        {
            Inputs = reader.ReadSerializableArray<TextInput>();
        }
    }
    public class TextInput : ISerializable
    {
        public string Text;
        public string FromName;
        public virtual int Size => Text.GetVarSize() + FromName.GetVarSize();


        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(Text);
            writer.WriteVarString(FromName);
        }
        public void Deserialize(BinaryReader reader)
        {
            Text = reader.ReadVarString();
            FromName = reader.ReadVarString();
        }
    }

    public static class TextInputHelper
    {
        public static void TextHandle(this TextInput textInput)
        {
            bool ok = false;
            if (SecureHelper.BlockIndex.IsNotNull())
            {
                if (SecureHelper.IsPort() || SecureHelper.IsAgent())
                {
                    if (!ok)
                    {
                        var builder = new PortInBoundBuilder(textInput);
                        var inBoundOrder = builder.Build();
                        if (inBoundOrder.IsNotNull())
                        {
                            if (builder.GetInboundOrder(inBoundOrder.OrderHead).IsNotNull())
                            {
                                MainForm.Instance.lb_msg.Text = UIHelper.LocalString($"微信订单号重复", $" wechat order id confilict");
                                ok = true;
                            }
                            else
                            {
                                builder.SaveInboundOrder(inBoundOrder);
                                var msg = UIHelper.LocalString($"微信订单:  {inBoundOrder.OrderHead.CNO}  已入库", $"received wechat order:{inBoundOrder.OrderHead.CNO}");
                                foreach (var module in PageTreeNode.Modules.Values)
                                {
                                    MainForm.Instance.DoInvoke(() =>
                                    {
                                        module.OnClipboardString(ClipboardMessageType.MarkOrder, textInput.Text);
                                    });
                                }
                                NoticeForm.Instance.Append(msg);
                            }
                            ok = true;
                        }
                    }
                    if (!ok)
                    {
                        try
                        {
                            var bs = Convert.FromBase64String(textInput.Text);
                            if (bs.TryAsSerializable<TextInputArray>(out var inputArray))
                            {
                                foreach (var subTextInput in inputArray.Inputs)
                                {
                                    var builder = new TextInBoundBuilder(subTextInput);
                                    var inBoundOrder = builder.Build();
                                    if (inBoundOrder.IsNotNull())
                                    {
                                        if (builder.GetInboundOrder(inBoundOrder.OrderHead).IsNotNull())
                                        {
                                            MainForm.Instance.lb_msg.Text = UIHelper.LocalString($"微信订单号重复", $" wechat order id confilict");
                                            ok = true;
                                        }
                                        else
                                        {
                                            builder.SaveInboundOrder(inBoundOrder);
                                            var msg = UIHelper.LocalString($"微信订单:  {inBoundOrder.OrderHead.CNO}  已入库", $"received wechat order:{inBoundOrder.OrderHead.CNO}");
                                            foreach (var module in PageTreeNode.Modules.Values)
                                            {
                                                MainForm.Instance.DoInvoke(() =>
                                                {
                                                    module.OnClipboardString(ClipboardMessageType.MarkOrder, textInput.Text);
                                                });
                                            }
                                            NoticeForm.Instance.Append(msg);
                                        }

                                    }
                                }
                                ok = true;
                            }
                        }
                        catch
                        {

                        }
                    }
                    if (!ok)
                    {
                        var builder = new TextInBoundBuilder(textInput);
                        var inBoundOrder = builder.Build();
                        if (inBoundOrder.IsNotNull())
                        {
                            if (builder.GetInboundOrder(inBoundOrder.OrderHead).IsNotNull())
                            {
                                MainForm.Instance.lb_msg.Text = UIHelper.LocalString($"微信订单号重复", $" wechat order id confilict");
                                ok = true;
                            }
                            else
                            {
                                builder.SaveInboundOrder(inBoundOrder);
                                var msg = UIHelper.LocalString($"微信订单:  {inBoundOrder.OrderHead.CNO}  已入库", $"received wechat order:{inBoundOrder.OrderHead.CNO}");
                                foreach (var module in PageTreeNode.Modules.Values)
                                {
                                    MainForm.Instance.DoInvoke(() =>
                                    {
                                        module.OnClipboardString(ClipboardMessageType.MarkOrder, textInput.Text);
                                    });
                                }
                                NoticeForm.Instance.Append(msg);
                                ok = true;
                            }

                        }
                    }
                }
            }
        }
    }
}
