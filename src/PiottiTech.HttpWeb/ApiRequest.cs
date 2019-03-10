using PiottiTech.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PiottiTech.HttpWeb
{
    public class ApiRequest
    {
        public HttpWebRequest HttpWebRequest { get; private set; }

        public ApiRequest(string url)
        {
            HttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //DEVNOTE: Use proxy server if present in config
            string proxyServerUrl = Config.AppSetting("proxyServerUrl");
            if (!String.IsNullOrEmpty(proxyServerUrl)) { HttpWebRequest.Proxy = new WebProxy(proxyServerUrl); }
        }

        public ApiRequest(string url, Dictionary<string, string> querystringData) : this(url + "?" + querystringData.ToHttpDataString())
        {
        }

        #region Post

        public ApiResponse Post(Dictionary<string, string> postData)
        {
            return Post(postData.ToHttpDataString());
        }

        public ApiResponse Post(string postData)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(postData);
            return Post(bytes);
        }

        public ApiResponse Post(byte[] postData)
        {
            HttpWebRequest.Method = "POST";
            HttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebRequest.ContentLength = postData.Length;

            Stream newStream = HttpWebRequest.GetRequestStream();
            newStream.Write(postData, 0, postData.Length);
            newStream.Close();

            HttpWebResponse httpWebResponse = (HttpWebResponse)HttpWebRequest.GetResponse();
            ApiResponse apiResponse = new ApiResponse(httpWebResponse);

            return apiResponse;
        }

        #endregion Post

        #region Get

        public ApiResponse Get()
        {
            HttpWebRequest.Method = "GET";

            HttpWebResponse httpWebResponse = (HttpWebResponse)HttpWebRequest.GetResponse();
            ApiResponse apiResponse = new ApiResponse(httpWebResponse);

            return apiResponse;
        }

        #endregion Get
    }
}

