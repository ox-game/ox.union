using OX.BMS;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Sunny.UI;

namespace OX.Tablet.UIs.Mark.MarkSix
{
    public static class AwardHelper
    {
        public static void CreateBitmap(MarkTerm term, PictureBox box)
        {

            using (Bitmap bmp = new Bitmap(500, 700))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    using var TempFont = new Font("黑体", 14F);

                    int mx = 1;
                    int my = 1;
                    string s = "我们的祖国是花园";
                    Size msf = TextRenderer.MeasureText(s, TempFont);
                    int msfMax = Math.Max(msf.Width, msf.Height);
                    g.FillRectangle(Color.DarkOrange, mx, my, msfMax, msf.Height + 1);
                    g.DrawString(s, TempFont, Color.White, new Rectangle(mx, my, msfMax, msf.Height), ContentAlignment.MiddleCenter);




                    //// 设置背景颜色
                    //g.Clear(Color.LightBlue);
                    //// 设置文字格式
                    //StringFormat format = new StringFormat();
                    //format.Alignment = StringAlignment.Center;
                    //format.LineAlignment = StringAlignment.Center;

                    //var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
                    //if (bmsIndex.IsNotNull())
                    //{
                    //    foreach (var r in NoneFlagEnumHelper.All<MarkSixRound>())
                    //    {
                    //        GuessAnswerKey key = new GuessAnswerKey { Term = term, ChannelRound = new MarkChannelRound(BetChannel.MarkSix, (byte)r) };
                    //        if (bmsIndex.GuessAnswers.TryGetValue(key.ToString(), out var answer))
                    //        {
                    //            //if (r == MarkSixRound.MarkHK)
                    //            //{
                    //            //    var s = $"{answer.Value.P1},{answer.Value.P2},{answer.Value.P3},{answer.Value.P4},{answer.Value.P5},{answer.Value.P6}+{answer.Value.T}";
                    //            //    // 绘制文字
                    //            //    g.DrawString(s, new Font("Arial", 20), Brushes.Black, new RectangleF(0, 0, bmp.Width, bmp.Height), format);
                    //            //}
                    //            //else if (r == MarkSixRound.MarkMacau)
                    //            //{
                    //            //    var s = $"{answer.Value.P1},{answer.Value.P2},{answer.Value.P3},{answer.Value.P4},{answer.Value.P5},{answer.Value.P6}+{answer.Value.T}";
                    //            //    // 绘制文字
                    //            //    g.DrawString(s, new Font("Arial", 20), Brushes.Black, new RectangleF(0, 100, bmp.Width, bmp.Height), format);
                    //            //}
                    //            //else if (r == MarkSixRound.MarkUnion)
                    //            //{
                    //            //    var s = $"{answer.Value.P1},{answer.Value.P2},{answer.Value.P3},{answer.Value.P4},{answer.Value.P5},{answer.Value.P6}+{answer.Value.T}";
                    //            //    // 绘制文字
                    //            //    g.DrawString(s, new Font("Arial", 20), Brushes.Black, new RectangleF(0, 200, bmp.Width, bmp.Height), format);
                    //            //}
                    //        }
                    //    }
                    //}
                }
                box.Image = bmp;
            }
        }
    }
}
