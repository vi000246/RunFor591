using System;
using System.Collections.Generic;
using System.IO;
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
            var archiveHouse = GetAllDataFromDB();
            var ShouldAlertHouse = GetShouldAlertHouse(matchHouse, archiveHouse);
            if (matchHouse.Any())
            {
                //取得這筆物件被存到db的時間
                var createTime = archiveHouse.houseList?.Where(x => x.postId == x.postId).FirstOrDefault().createTime;
                Helper.WriteMultipleLineLig("新物件列表(排除尚未通知物件):", matchHouse.Except(ShouldAlertHouse)
                    .Select(x => 
                        x.title +
                        (createTime.HasValue ? (" DB createTime:" + createTime.Value.ToString("yyyy/MM/dd HH:mm:ss")):"") +
                        " URL:" + x.houseUrl).ToList(), log);
            }
            else
            {
                log.Info("找不到符合條件的最新物件");
            }

            if (ShouldAlertHouse.Any())
            {
                Helper.WriteMultipleLineLig("需要發送通知的物件列表:",
                    ShouldAlertHouse.Select(x => x.title + "url:" + x.houseUrl).ToList(), log);
            }

            PubMessageToNotifiter(ShouldAlertHouse);
            SyncDataToDb(ShouldAlertHouse);
        }

        public IEnumerable<houseEntity> GetHouseList(string csrfToken)
        {
            var urls = new UrlGenerator().GetHouseListApiUrl();
            Helper.WriteMultipleLineLig("Request urls",urls.ToList(),log);
            IEnumerable<houseEntity> houseList = new List<houseEntity>();
            foreach (var url in urls)
            {
                try
                {
                    var response = Get591Response(url, Method.POST, csrfToken).Content;
                    var houseResponse = JsonConvert.DeserializeObject<ResponseHouseEntity>(response);
                    var entity = Convert591DataToEntity(houseResponse.data.data);
                    houseList = houseList.Concat(entity);
                }
                catch (Exception ex)
                {
                    log.Error("GetHouseList error. msg:"+ex.Message);
                }
            }
            
            return houseList;
        }

        public PhotoListResponse GetPhotoList(houseEntity house)
        {
            var url = UrlGenerator._BaseUrl + UrlGenerator._PhotoListUrl + $"?post_id={house.post_id}&type=1";
            var response = Get591Response(url, Method.GET).Content;
            var PhotoListResponse = new PhotoListResponse();
            try
            {
                PhotoListResponse = JsonConvert.DeserializeObject<PhotoListResponse>(response);
            }
            catch
            {
                return null;
            }

            return PhotoListResponse;
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
                entity.layout = obj.layout;
                entity.area = obj.area.ToString();


                entityList.Add(entity);
            }
            

            return entityList;
        }

        public ArchiveHouse GetAllDataFromDB()
        {
            var dbClient = AutoFacUtility.Container.Resolve<IDataBase>();
            return dbClient.GetAllDataFromDB();
        }

        //取得已發送過通知的房屋列表，並過濾出沒發送通知的列表
        public IEnumerable<houseEntity> GetShouldAlertHouse(IEnumerable<houseEntity> houseList,ArchiveHouse archiveHouse)
        {
            if (archiveHouse.houseList != null && archiveHouse.houseList.Count > 0)
            {
                var archiveIds = archiveHouse.houseList.Select(x => x.postId).ToList();
                return houseList.Where(x => !archiveIds.Contains(x.post_id));
            }
            else
            {
                return houseList;
            }
        }

        //將已發送過通知的房屋存到db,並刪掉已過期的
        public void SyncDataToDb(IEnumerable<houseEntity> houseList)
        {
            var dbClient = AutoFacUtility.Container.Resolve<IDataBase>();
            dbClient.UpdateData(houseList.Select(x=>x.post_id).ToList());
        }

        public void PubMessageToNotifiter(IEnumerable<houseEntity> houseEntity)
        {
            var notifyService = AutoFacUtility.Container.Resolve<INotify>();
            foreach (var house in houseEntity)
            {
                var photoList = GetPhotoList(house);
                notifyService.PubNotify(house,photoList);
            }
            
        }

        //取出狀態為new的房屋物件
        public IEnumerable<houseEntity> FilterHouse(IEnumerable<houseEntity> houseList)
        {
            return houseList.Where(x=> x.isNew);
        }
    }
}
