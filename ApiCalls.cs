using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CoinbaseProAPI
{
    public class ApiCalls
    {
        public static string coinbaseAuthTest(string method, string reqPath, string url, string body, string timestamp, string apiKey, string sign, string passphrase)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + reqPath);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;
            httpWebRequest.Headers.Add("CB-ACCESS-KEY", apiKey);
            httpWebRequest.Headers.Add("CB-ACCESS-SIGN", sign);
            httpWebRequest.Headers.Add("CB-ACCESS-TIMESTAMP", timestamp); 
            httpWebRequest.Headers.Add("CB-ACCESS-PASSPHRASE", passphrase);
            httpWebRequest.Headers.Add("User-Agent", "curl");
            WebProxy myproxy = new WebProxy("127.0.0.1", 9095);
            myproxy.BypassProxyOnLocal = false;
            httpWebRequest.Proxy = myproxy;

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JArray bd = JArray.Parse(result);

                var responseCode = bd["responseStatusCode"].ToString();
                return responseCode;
            }
        }


        //GET Accounts
        public static string accounts(string url, string apiKey, string passphrase, string apiSecret)
        {
            Int32 time = Auth.timestamp();
            Double timeDouble = Convert.ToDouble(time);
            string timeString = timeDouble.ToString();
            string method = "GET";
            string reqPath = "/accounts/";
            string body = "";
            string sign = Auth.ComputeSignature(method, apiSecret, timeDouble, reqPath, body);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + reqPath);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;
            httpWebRequest.Headers.Add("CB-ACCESS-KEY", apiKey);
            httpWebRequest.Headers.Add("CB-ACCESS-SIGN", sign);
            httpWebRequest.Headers.Add("CB-ACCESS-TIMESTAMP", timeString);
            httpWebRequest.Headers.Add("CB-ACCESS-PASSPHRASE", passphrase);
            httpWebRequest.Headers.Add("User-Agent", "curl");
            WebProxy myproxy = new WebProxy("127.0.0.1", 9095);
            myproxy.BypassProxyOnLocal = false;
            httpWebRequest.Proxy = myproxy;

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JArray bd = JArray.Parse(result);

                var responseCode = bd["responseStatusCode"].ToString();
                return responseCode;
            }
        }



      


    }
}
