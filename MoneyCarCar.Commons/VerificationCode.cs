using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace MoneyCarCar.Commons
{
    public class VerificationCode
    {
        private static Random objRandom = new Random();

        //产生图片 宽度：_WIDTH, 高度：_HEIGHT
        private static readonly int _WIDTH = 130, _HEIGHT = 34;
        //字体集
        //private static readonly string[] _FONT_FAMIly = { "Arial", "Arial Black", "Arial Italic", "Courier New", "Courier New Bold Italic", "Courier New Italic", "Courier New Italic", "Courier New Bold Italic" };
        private static readonly string[] _FONT_FAMIly = { "Arial", "Arial Black", "Arial Italic", "Courier New", "Courier New Bold Italic", "Courier New Italic", "Franklin Gothic Medium", "Franklin Gothic Medium Italic" };
        //字体大小集
        private static readonly int[] _FONT_SIZE = { 15, 20, 25 };
        //前景字体颜色集
        private static readonly Color[] _COLOR_FACE = { Color.FromArgb(113, 153, 67), Color.FromArgb(30, 99, 140), Color.FromArgb(206, 60, 19), Color.FromArgb(227, 60, 0) };
        //背景颜色集
        private static readonly Color[] _COLOR_BACKGROUND = { Color.FromArgb(247, 254, 236), Color.FromArgb(234, 248, 255), Color.FromArgb(244, 250, 246), Color.FromArgb(248, 248, 248) };
        //文本布局信息
        private static StringFormat _DL_FORMAT = new StringFormat(StringFormatFlags.NoClip);
        //左右旋转角度
        private static readonly int _ANGLE = 30;

        public static string GetCheckCode(int vcodeLength = 4)
        {
            string verifyCodeText = "";
            StringBuilder objStringBuilder = new StringBuilder();
            //加入数字1-9 
            for (int i = 1; i <= 9; i++)
            {
                if (i != 0 && i != 1)
                    objStringBuilder.Append(i.ToString());
            }
            //加入大写字母A-Z，不包括O 

            char temp = ' ';
            for (int i = 0; i < 26; i++)
            {
                temp = Convert.ToChar(i + 65);
                //如果生成的字母不搜索是'O' 
                if (!temp.Equals('O') && !temp.Equals('L'))
                {
                    objStringBuilder.Append(temp);
                }
            }

            //加入小写字母a-z，不包括o 

            for (int i = 0; i < 26; i++)
            {
                temp = Convert.ToChar(i + 97);
                //如果生成的字母不是'o' 
                if (!temp.Equals('o') && !temp.Equals('l'))
                {
                    objStringBuilder.Append(temp);
                }
            }
            //生成验证码字符串 
            {
                int index = 0;
                for (int i = 0; i < vcodeLength; i++)
                {
                    index = objRandom.Next(0, objStringBuilder.Length);
                    verifyCodeText += objStringBuilder[index];
                    objStringBuilder.Remove(index, 1);
                }
            }
            return verifyCodeText;
        }

        public static byte[] CreateImage(string code)
        {
            byte[] result;
            _DL_FORMAT.Alignment = StringAlignment.Center;
            _DL_FORMAT.LineAlignment = StringAlignment.Center;

            long tick = DateTime.Now.Ticks;
            Random Rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            using (Bitmap _img = new Bitmap(_WIDTH, _HEIGHT))
            {
                using (Graphics g = Graphics.FromImage(_img))
                {
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                    Point dot = new Point(20, 20);

                    // 定义一个无干扰线区间和一个起始位置
                    int nor = Rnd.Next(53), rsta = Rnd.Next(130);
                    // 绘制干扰正弦曲线 M:曲线平折度, D:Y轴常量 V:X轴焦距
                    int M = Rnd.Next(15) + 5, D = Rnd.Next(20) + 15, V = Rnd.Next(5) + 1;

                    int ColorIndex = Rnd.Next(4);

                    float Px_x = 0.0F;
                    float Px_y = Convert.ToSingle(M * Math.Sin(V * Px_x * Math.PI / 180) + D);

                    //填充背景
                    g.Clear(_COLOR_BACKGROUND[Rnd.Next(4)]);

                    //前景刷子 //背景刷子
                    using (Brush _BrushFace = new SolidBrush(_COLOR_FACE[ColorIndex]))
                    {
                        //初始化光标的开始位置
                        g.TranslateTransform(8, 0);

                        #region 绘制校验码字符串
                        for (int i = 0; i < code.Length; i++)
                        {
                            //随机旋转 角度
                            int angle = Rnd.Next(-_ANGLE, _ANGLE);
                            //移动光标到指定位置
                            g.TranslateTransform(dot.X, dot.Y);
                            //旋转
                            g.RotateTransform(angle);

                            //初始化字体
                            using (Font _font = new Font(_FONT_FAMIly[Rnd.Next(0, 8)], _FONT_SIZE[Rnd.Next(0, 3)]))
                            {
                                //绘制
                                g.DrawString(code[i].ToString(), _font, _BrushFace, 1, 1, _DL_FORMAT);
                            }
                            //反转
                            g.RotateTransform(-angle);
                            //重新定位光标位置
                            g.TranslateTransform(-2, -dot.Y);
                        }
                        #endregion

                    }
                }

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    _img.Save(ms, ImageFormat.Png);
                    result = ms.GetBuffer();
                    ms.Flush();
                }
            }
            return result;
        }

    }
}
