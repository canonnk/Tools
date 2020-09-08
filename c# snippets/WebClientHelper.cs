using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class WebClientHelper {
        public static void UploadFile(string url, string filePath, Dictionary<string, string> para) {
            WebClient client = new WebClient( );
            client.Credentials = CredentialCache.DefaultCredentials;
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            foreach (var item in para) {
                client.QueryString[item.Key] = item.Value;
            }
            client.UploadFile(url, "POST", filePath);
        }

    }
}
