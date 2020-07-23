using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RunFor591.CrawlerUtility;
using RunFor591.Entity;

namespace RunFor591
{
    public class Crawler
    {
        private RestClient _591client;

        public Crawler()
        {
            _591client = new RestClient(UrlGenerator._BaseUrl);
            _591client.CookieContainer = new System.Net.CookieContainer();
        }

        public void StartCrawl591()
        {
            var csrfToken = GetCSRFToken();
            var houseList = GetHouseList(csrfToken);
            var matchHouse = FilterHouse();
            var ShouldAlertHouse = SyncDataFromDB();
            //call notify service
        }

        public string GetHouseList(string csrfToken)
        {
            var urls = new UrlGenerator().GetHouseListApiUrl();
            foreach (var url in urls)
            {
                var response = Get591Response(url, Method.POST,csrfToken).Content;
                var houseResponse = JsonConvert.DeserializeObject<ResponseHouseEntity>(response);
            }
            
            return "";
        }

        public string GetCSRFToken()
        {
            var res = Get591Response(null);
            return GetCSRFtokenFromHtml(res.Content);
        }

        public IRestResponse Get591Response(string routeUrl,Method method = Method.GET,string token = null)
        {
            RestRequest request;
            if (string.IsNullOrEmpty(routeUrl))
            {
                request = new RestRequest();
            }else
            {
                request = new RestRequest(routeUrl);
            }

            request.Method = method;
            if(token != null)
                request.AddHeader("X-CSRF-TOKEN", token);
            var response = _591client.Get(request);
            return response;
        }

        public string GetCSRFtokenFromHtml(string html)
        {
            var tokenValue = "";
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNode mdnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='csrf-token']");

            if (mdnode != null)
            {
                HtmlAttribute desc;

                desc = mdnode.Attributes["content"];
                tokenValue = desc.Value;
            }

            return tokenValue;
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

        public string SyncDataFromDB()
        {
            return "";
        }

        public string FilterHouse()
        {
            return "";
        }
    }
}
