using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace CoinbaseProAPI
{
    public class Auth
    {
        public static Int32 timestamp()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //string timeStampString = unixTimestamp.ToString();
            return unixTimestamp;

        }

        public static string ComputeSignature(
            string method,
            string secret,
            double timestamp,
            string requestUri,
            string contentBody = "")
        {
            var convertedString = Convert.FromBase64String(secret);
            var prehash = timestamp.ToString("F0", CultureInfo.InvariantCulture) + method + requestUri + contentBody;
            return HashString(prehash, convertedString);
        }

        private static string HashString(string str, byte[] secret)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var hmaccsha = new HMACSHA256(secret))
            {
                return Convert.ToBase64String(hmaccsha.ComputeHash(bytes));
            }
        }
    }
}
