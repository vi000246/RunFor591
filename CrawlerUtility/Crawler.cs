using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using HtmlAgilityPack;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RunFor591.Common;
using RunFor591.CrawlerUtility;
using RunFor591.Entity;
using RunFor591.NotifyUtility;

namespace RunFor591
{
    public class Crawler
    {
        private RestClient _591client;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Crawler()
        {
            _591client = new RestClient(UrlGenerator._BaseUrl);
            _591client.CookieContainer = new System.Net.CookieContainer();
        }

        public void StartCrawl591()
        {
            var csrfToken = GetCSRFToken();
            var houseList = GetHouseList(csrfToken);
            var matchHouse = FilterHouse(houseList);
            Helper.WriteMultipleLineLig("New House List", matchHouse.Select(x=>x.title + "url:"+x.houseUrl).ToList(), log);
            var ShouldAlertHouse = GetShouldAlertHouse(matchHouse);
            PubMessageToNotifiter();
        }

        public IEnumerable<houseEntity> GetHouseList(string csrfToken)
        {
            var urls = new UrlGenerator().GetHouseListApiUrl();
            Helper.WriteMultipleLineLig("Request urls",urls.ToList(),log);
            IEnumerable<houseEntity> houseList = new List<houseEntity>();
            foreach (var url in urls)
            {
                var response = Get591Response(url, Method.POST,csrfToken).Content;
                var houseResponse = JsonConvert.DeserializeObject<ResponseHouseEntity>(response);
                var entity = Convert591DataToEntity(houseResponse.data.data);
                houseList = houseList.Concat(entity);
            }
            
            return houseList;
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

        //公司不能用json bin...
//        public string GetJsonBinExample()
//        {
//            var client = new RestClient("https://api.jsonbin.io/b/5f19128591806166284734fc");
//            var request = new RestRequest();
//            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
//            request.Method = Method.GET;
//            request.AddHeader("secret-key", "$2b$10$nH0piEsV0NDZi1jWWc185emNZ2.5sohh0iNhR5yuHqfK5vi051tCu");
//            var response = client.Get(request);
//            return response.Content;
//        }

        public IEnumerable<houseEntity> Convert591DataToEntity(IEnumerable<Datum> objlist)
        {
            var entityList = new List<houseEntity>();
            foreach (var obj in objlist)
            {
                var entity = new houseEntity();
                entity.address = obj.address+" :"+obj.regionname+obj.sectionname+obj.street_name+obj.alley_name + obj.addr_number_name;
                entity.title = obj.address_img;
                entity.floorInfo = obj.floorInfo;
                entity.isNew = obj.new_img.Length > 0;
                entity.coverUrl = obj.cover;
                entity.album = obj.house_img.Split(',').ToList();//有個問題，可能會有空的array item
                entity.post_id = obj.post_id;
                entity.price = obj.price;
                entity.regionName = obj.regionname;
                entity.updateTime = DateTimeOffset.FromUnixTimeSeconds(obj.updatetime).UtcDateTime;
                entity.refreshTime = DateTimeOffset.FromUnixTimeSeconds(obj.refreshtime).UtcDateTime;
                entity.sectionname = obj.sectionname;
                entity.addr_number_name = obj.addr_number_name;
                entity.alley_name = obj.alley_name;
                entity.street_name = obj.street_name;
                entity.kind_name = obj.kind_name;


                entityList.Add(entity);
            }
            

            return entityList;
        }

        //取得尚未發送過通知的房屋列表
        public IEnumerable<houseEntity> GetShouldAlertHouse(IEnumerable<houseEntity> houseList)
        {
            
            return houseList;
        }

        //將已發送過通知的房屋儲存至db
        public void StoreDataToDb(IEnumerable<houseEntity> houseList)
        {

        }

        public void PubMessageToNotifiter()
        {
            var notifyService = AutoFacUtility.Container.Resolve<INotify>();
            notifyService.PubMessage("");
        }

        //取出狀態為new的房屋物件
        public IEnumerable<houseEntity> FilterHouse(IEnumerable<houseEntity> houseList)
        {
            return houseList.Where(x=> x.isNew);
        }
    }
}
