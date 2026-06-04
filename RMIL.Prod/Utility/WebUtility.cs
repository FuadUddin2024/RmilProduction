using System;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace RMIL.Prod.Utility
{
    public static class WebUtility
    {
        #region Variables
        public static string UserNameForSms = ConfigurationManager.AppSettings["SMSGatewayUserID"];
        public static string Password = ConfigurationManager.AppSettings["SMSGatewayPassword"];
        public static string Title = ConfigurationManager.AppSettings["SMSGatewayTitle"];
      
        public static string SessionCurrentUserObj = "SessionCurrentUserObj";
        public static string SessionID = "SessionID";
        public static string Username = "username";
        public static string GlobalTheme = "GlobalTheme";

        #endregion
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static string GetIpAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static void RmilServiceSmsWithUnicode(string mobileNumber, string message)
        {
            try
            {
                //http://sms.prangroup.com/postman/api/sendsms?userid=relservice_api&password=bH9fVb0&msisdn=01992659242&masking=28585&message=Welcome
                string url = "http://sms.prangroup.com/postman/api/sendsms?userid=relservice_api&unicode=true&password=" + MD5Hash("bH9fVb0") + "&msisdn=" + mobileNumber + "&masking=" + 28585 + "&message=" + message;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();

            }
            catch (Exception ex)
            {

            }
        }
        public static void SendSms(string username, string password, string senderTitle, string mobileNumber, string message)
        {
            try
            {
                //string url = "http://172.17.4.97:13014/cgi-bin/sendsms?username=smsgw&password=smsgw&from=PRPS&to=01912257257&coding=2&charset=UTF-8&text=Dear Sir, This message Generated from RMIL Service Management System";
                string url = "http://172.17.4.97:13014/cgi-bin/sendsms?username=" + username + "&password=" + password + "&from=" + senderTitle + "&to=" + mobileNumber + "&coding=2&charset=UTF-8&text=" + message;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (Exception ex)
            {
                
            }
        }
        public static void SendSms1(string mobileNumber, string message, string userid, string password, string senderTitle)
        {
            try
            {
                string url = "https://vas.banglalinkgsm.com/sendSMS/sendSMS?msisdn=88" + mobileNumber + "&message=" + message + "&userID=" + userid + "&passwd=" + password + "&sender=" + senderTitle;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (Exception ex)
            {

            }
        }
        public static void SmsSend(string mobileNumber, string message, string senderTitle)
        {
            try
            {
                string url = "http://172.17.2.107/xx.aspx?username=lsmsprg&pasx=123456EWD3215gf432yu&mobile=" + mobileNumber + "&msg=" + message + "&senderTitle=" + senderTitle;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (Exception ex)
            {

            }
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}