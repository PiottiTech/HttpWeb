using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PiottiTech.HttpWeb
{
    public static class ExtensionMethods
    {
        public static string ToHttpDataString(this Dictionary<string, string> dictionary)
        {
            StringBuilder sb = new StringBuilder(32);

            bool firstPass = true;

            foreach (KeyValuePair<string, string> entry in dictionary)
            {
                if (!firstPass)
                {
                    sb.Append("&");
                }
                sb.Append(entry.Key);
                sb.Append("=");
                sb.Append(WebUtility.UrlEncode((string)entry.Value));
                firstPass = false;
            }
            return sb.ToString();
        }
    }
}