using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OHM.Tools
{
    public class FileTypeHelper
    {

        public static string GetFileContentType(string FileName)
        {
            // 拼凑OWA需要的请求头内容类型
            string contentType = "";
            switch (FileName.Substring(FileName.LastIndexOf('.')))
            {
                case ".xls":
                    contentType = "application/vnd.ms-excel";
                break;
                case ".xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                break;
                case ".ppt":
                    contentType = "application/vnd.ms-powerpoint";
                break;
                case ".pptx":
                    contentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                break;
                case ".doc":
                    contentType = "application/msword";
                break;
                case ".docx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                break;
                case ".pdf":
                    contentType = "application/pdf";
                break;
            }
            return contentType;
        }
    }
}


