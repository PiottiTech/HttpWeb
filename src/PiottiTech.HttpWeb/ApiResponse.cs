using Newtonsoft.Json;
using PiottiTech.Common;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace PiottiTech.HttpWeb
{
    public class ApiResponse
    {
        public HttpWebResponse HttpWebResponse { get; private set; }

        public String ResponseString { get; private set; } = String.Empty;

        public bool Success { get; private set; }

        public ApiResponse(HttpWebResponse httpWebResponse)
        {
            HttpWebResponse = httpWebResponse;
            Success = (HttpWebResponse.StatusCode == HttpStatusCode.OK);

            Encoding encoding = System.Text.Encoding.GetEncoding(1252);
            StreamReader streamReader = new StreamReader(HttpWebResponse.GetResponseStream(), encoding);
            ResponseString = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
        }

        public ApiResult ToApiResult()
        {
            return JsonConvert.DeserializeObject<ApiResult>(ResponseString);
        }
    }
}
