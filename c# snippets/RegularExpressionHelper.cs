using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class RegularExpressionHelper {

        /// <summary>
        /// 判断是否为需要的文件（基于扩展名）
        /// </summary>
        /// <param name="extNames">扩展名列表，例如：.xls|.xlsx|.pdf</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool IsFileType(string extNames, string fileName) {
            return Regex.IsMatch(fileName, "(" + extNames + ")$");
        }

        /// <summary>
        /// 是否excel、ppt、word文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsOfficeFile(string fileName) {
            return IsFileType(".xls|.xlsx|.ppt|.pptx|.doc|.docx", fileName);
        }

        /// <summary>
        /// 是否图片文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsImageFile(string fileName) {
            return IsFileType(".jpg|.png|.bpm|.gif", fileName);
        }
    }
}
