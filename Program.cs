using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace CoinbaseProAPI
{
    class Program
    {
        static void Main(string[] args)
        {            
            string apiKey = args[0];
            string apiSecret = args[1];
            string passPhrase = args[2];
            Int32 time = Auth.timestamp();
            Double timeDouble = Convert.ToDouble(time);
            string method = "GET";
            string url = "http://api.pro.coinbase.com";

            //string requestPath = "/accounts/";
            //string body = "";
            //string message = time + method + requestPath + body;
            //string sign = Auth.ComputeSignature(method, apiSecret, time, requestPath, body);

            //ApiCalls.coinbaseAuthTest(method, requestPath, url, body, timeDouble.ToString(), apiKey, sign, passPhrase);

            //ApiCalls.accounts(url, apiKey, passPhrase, apiSecret);
            ProductsApiCalls.products(url, apiKey, passPhrase, apiSecret);

        }
    }
}
