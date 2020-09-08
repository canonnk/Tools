using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class SHA1 {
        private SHA1( ) { }

        // 使用sha1加密数据
        public static string Sha1Encrypt(string str) {
            byte[] StrRes = Encoding.Default.GetBytes(str);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider( );
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder( );
            foreach (byte iByte in StrRes) {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString( );
        }

        // 加密str后判断是否与sha1Str相等
        public static bool Sha1Compare(string str, string sha1Str) {
            return Sha1Encrypt(str) == sha1Str;
        }
    }
}
