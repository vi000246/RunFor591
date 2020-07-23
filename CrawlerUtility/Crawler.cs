using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace RunFor591
{
    public class Crawler
    {

        //需要loop取得整層住家/套房/獨立套房的data
        public string GetRawContext()
        {
            var client = new RestClient("https://api.jsonbin.io/b/5f19128591806166284734fc");
            var request = new RestRequest();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            request.Method = Method.GET;
            request.AddHeader("secret-key", "$2b$10$nH0piEsV0NDZi1jWWc185emNZ2.5sohh0iNhR5yuHqfK5vi051tCu");
            var response = client.Get(request);
            return response.Content;
        }

        //不知道是公司封鎖 還是我的問題 先留起來備用
        public string GetJsonBinExample()
        {
            var client = new RestClient("https://api.jsonbin.io/b/5f19128591806166284734fc");
            var request = new RestRequest();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            request.Method = Method.GET;
            request.AddHeader("secret-key", "$2b$10$nH0piEsV0NDZi1jWWc185emNZ2.5sohh0iNhR5yuHqfK5vi051tCu");
            var response = client.Get(request);
            return response.Content;
        }

        public void ConvertRawDataToObj()
        {

        }

        public void FilterHouse()
        {

        }
    }
}
