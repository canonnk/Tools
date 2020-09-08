using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools
{
    public class HeadPortraitHelper
    {
        /// <summary>
        /// 生成人员的默认头像路径
        /// </summary>
        /// <param name="personname"></param>
        /// <param name="personcode"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string  UpdateHeadPortrait(string personname, string personcode)
        {
            //var colors = ConfigurationManager.AppSettings["head_color"].Split(';');
            var colors = SystemGlobalConfiguration.GetInstance( )["head_color"].Split(';');
            List<Color> color = new List<Color>();
            foreach (var item in colors)
            {
                color.Add(Color.FromArgb(int.Parse(item.Split(',')[0]), int.Parse(item.Split(',')[1]), int.Parse(item.Split(',')[2])));
            }
            Random random = new Random();
            var index = random.Next(0, color.Count - 1);
            var a = color[index];

            //画图
            Bitmap bmp = new Bitmap(100, 100);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            SolidBrush brush1 = new SolidBrush(a);
            g.FillRectangle(brush1, 0, 0, 100, 100);

            var Name = personname.Replace(" ", "");
            var Num1 = 2;
            var Num2 = 1;
            //判断字符串长度，并加上文字
            if (Name.Length == Num1)
            {
                g.DrawString(Name, new Font("微软雅黑", 30), Brushes.White, new PointF(4, 22));
            }
            else
            {
                if (Name.Length == Num2)
                {
                    g.DrawString(Name, new Font("微软雅黑", 30), Brushes.White, new PointF(22, 22));
                }
                else
                {
                    string PersonName = Name.Remove(0, Name.Length - 2);
                    g.DrawString(PersonName, new Font("微软雅黑", 30), Brushes.White, new PointF(4, 22));
                }

            }
            MemoryStream ms = new MemoryStream();
            g.Dispose();

            //获取服务器上传地址
            var FilePath = SystemGlobalConfiguration.GetInstance()["file_parent_root"];
            //var FilePath = ConfigurationManager.AppSettings["file_parent_root"];

            var filePath = string.Format("{0}\\SystemFile\\HeadPortrait", FilePath);

            //判断是存在文件夹
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //保存指定路径
            string imagePath = null;
                string time = personcode + "_" + DateTime.Now.ToString("yyyyMMddhhHHmmss") + ".png";
                bmp.Save(filePath + "\\" + time);
                bmp.Dispose();
                imagePath = "\\SystemFile\\HeadPortrait\\" + time;
            return imagePath;
        }
    }
}
