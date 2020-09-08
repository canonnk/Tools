using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace OHM.WebApi.Infrastructure.Helper {
    /// <summary>
    /// 短信访问类
    /// </summary>
    public class SMSAccess {
        ///// <summary>
        ///// 用户ID
        ///// </summary>
        //public string UserId = "692926";
        ///// <summary>
        ///// 用户账号
        ///// </summary>
        //public string UserAccount = "admin";
        ///// <summary>
        ///// 用户密码
        ///// </summary>
        //public string UserPassword = "CULKN1";


        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId = "692988";
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount = "admin";
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword = "MA6ZSB";


        /// <summary>
        /// 用户令牌
        /// </summary>
        public string UserToken = "";
        /// <summary>
        /// 用户短信码
        /// </summary>
        public string UserSmsNums = "";
        private static HttpWebRequest httpWReq;
        private static HttpWebResponse httpWResp;
        private static CookieContainer cookie = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SMSAccess( ) {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// Cookie
        /// </summary>
        public CookieContainer Cookie {
            get {
                if (cookie == null) {
                    cookie = new CookieContainer( );
                }
                return cookie;
            }
        }
        /// <summary>
        /// 登陆短信服务器
        /// </summary>
        /// <returns></returns>
        public bool Login( ) {
            bool loginFlag = false;
            string url2 = "http://61.143.63.174:8080/GateWay/Services.asmx/UserLogin?UserID=" + UserId + "&Account=" + UserAccount + "&Password=" + UserPassword;
            string strResp = GetWebResponse(url2);
            if (strResp != "") {
                XmlDocument doc = new XmlDocument( );
                doc.LoadXml(strResp);
                XmlElement root = doc.DocumentElement;
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("sms", "LoginReturns");
                XmlNode LoginFlag = doc.SelectSingleNode("//sms:RetCode", nsmgr);
                XmlNode tokenstr = doc.SelectSingleNode("//sms:Token", nsmgr);
                XmlNode smsnums = doc.SelectSingleNode("//sms:SmsStock", nsmgr);
                if (LoginFlag.InnerText == "Sucess") {
                    loginFlag = true;
                    UserToken = tokenstr.InnerText;
                    UserSmsNums = smsnums.InnerText;
                } else {

                    loginFlag = false;

                }
            } else {
                loginFlag = false;
            }

            return loginFlag;

        }
        /// <summary>
        /// 用户登录注销 利用 全局变量 UserToken 传递参数
        /// </summary>
        public void UserLoginOut( ) {
            string posturl = "http://61.143.63.174:8080/GateWay/Services.asmx/UserLogOff?Token=" + UserToken;
            GetWebResponse(posturl);
        }
        /// <summary>
        /// 获取用户剩余短信条数 利用全局变量 UserSmsNums 传值
        /// </summary>
        /// <returns></returns>
        public string SmsStock( ) {
            Login( );
            UserLoginOut( );
            return UserSmsNums;
        }
        /// <summary>
        /// 不登录服务器发送短信
        /// </summary>
        /// <param name="mobile">接收手机号</param>
        /// <param name="msgcontent">短信内容</param>
        /// <param name="sendtime">发送时间</param>
        /// <returns></returns>
        public string SendMsg(string mobile, string msgcontent, string sendtime) {
            string rtustr = "";
            //string posurl = "http://www.mxtong.net.cn/gateway/Services.asmx/DirectSend?UserID=" +UserId + "&Account=" + UserAccount + "&Password=" + UserPassword + "&Phones=" + mobile + "&Content=" + msgcontent + "&SendTime=" + sendtime+ "&SendType=1&PostFixNumber=";
            string posurl = "http://61.143.63.174:8080/gateway/Services.asmx/DirectSend?UserID=" + UserId + "&Account=" + UserAccount + "&Password=" + UserPassword + "&Phones=" + mobile + "&Content=" + msgcontent + "&SendTime=" + sendtime + "&SendType=1&PostFixNumber=";
            string RtuContent = GetWebResponse(posurl);
            if (RtuContent != "") {
                XmlDocument doc = new XmlDocument( );
                doc.LoadXml(RtuContent);
                XmlElement root = doc.DocumentElement;
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("sms", "JobSendedDescription");
                XmlNode ErrorMessage = doc.SelectSingleNode("//sms:Message", nsmgr);
                XmlNode SendFlag = doc.SelectSingleNode("//sms:RetCode", nsmgr);
                if (SendFlag.InnerText == "Sucess") {
                    rtustr = "发送成功!";

                    #region 记录发送记录

                    #endregion
                } else {
                    rtustr = "发送失败!" + ErrorMessage.InnerText;
                }
            } else {
                rtustr = "发送失败!,服务器异常错误!";

            }
            return rtustr;
        }
        /// <summary>
        /// 修改用户账户密码
        /// </summary>
        /// <param name="OldPassword">旧密码</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        public string ChangeUserPassword(string OldPassword, string NewPassword) {
            string rtustr = "";
            Login( );
            string posturl = "http://61.143.63.174:8080//GateWay/Services.asmx/PasswordChange?Token=" + UserToken + "&oldPSW=" + OldPassword + "&newPSW=" + NewPassword;
            string RtuContent = GetWebResponse(posturl);
            if (RtuContent != null) {
                XmlDocument doc = new XmlDocument( );
                doc.LoadXml(RtuContent);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("sms", "CommonReturn");
                XmlNode Flag = doc.SelectSingleNode("//sms:RetCode", nsmgr);
                XmlNode ErrorMessage = doc.SelectSingleNode("//sms:Message", nsmgr);
                if (Flag.InnerText == "Sucess") {
                    rtustr = "密码修改成功!";
                } else {
                    rtustr = "密码修改失败!" + ErrorMessage.InnerText;
                }
            } else {
                rtustr = "服务器错误,密码修改失败!";

            }
            UserLoginOut( );
            return rtustr;

        }
        /// <summary>
        /// 获取回复短信
        /// </summary>
        /// <returns></returns>
        public string GetReplyMsg( ) {
            string rtustr = "";
            Login( );//登录，给UserToken 赋值
            string posturl = "http://61.143.63.174:8080/GateWay/Services.asmx/FetchSMS?Token=" + UserToken;
            string RtuContent = GetWebResponse(posturl);
            if (RtuContent != null) {
                XmlDocument doc = new XmlDocument( );
                doc.LoadXml(RtuContent);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("sms", "FetchSMSResponse");
                XmlNode Flag = doc.SelectSingleNode("//sms:RetCode", nsmgr);
                if (Flag.InnerText == "") {

                    XmlNode SmsContent = doc.SelectSingleNode("//sms:Nodes:SMSGroup", nsmgr);//短信内容
                    XmlNode phone = doc.SelectSingleNode("//sms:Nodes:SMSGroup.Phone", nsmgr);//回复的手机号
                    XmlNode rectime = doc.SelectSingleNode("//sms:Nodes:SMSGroup.RecDateTime", nsmgr);//回复时间
                    XmlNode jobid = doc.SelectSingleNode("//sms:Nodes:SMSGroup.PostFixNumber", nsmgr);//对应的任务ID
                    rtustr = SmsContent.InnerText + "|" + phone.InnerText + "|" + rectime.InnerText + "|" + jobid.InnerText;
                } else {
                    XmlNode ErrorMessage = doc.SelectSingleNode("//sms:Message", nsmgr);
                    rtustr = "获取用户回复短信失败!" + ErrorMessage.InnerText;

                }
            } else {
                rtustr = "操作失败!";

            }
            UserLoginOut( );
            return rtustr;

        }
        /// <summary>
        /// 提交一个URL至服务器上运行
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private string GetWebResponse(string uri) {
            string strResp = "";
            httpWReq = (HttpWebRequest) WebRequest.Create(uri);
            httpWReq.CookieContainer = Cookie;
            httpWResp = (HttpWebResponse) httpWReq.GetResponse( );
            using (StreamReader reader = new StreamReader(httpWResp.GetResponseStream( ), System.Text.Encoding.GetEncoding("utf-8"))) {
                strResp = reader.ReadToEnd( );
            }
            httpWResp.Close( );
            return strResp;
        }
    }
}