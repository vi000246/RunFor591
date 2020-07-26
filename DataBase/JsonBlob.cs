using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RunFor591.Common;
using RunFor591.CrawlerUtility;
using RunFor591.Entity;

namespace RunFor591
{
    //使用json hosting當作database
    public class JsonBlob:IDataBase
    {
        private string jsonBlobUrl = "https://jsonblob.com/api/jsonBlob/";

        private IRestResponse makeApiRequest(string routeUrl, Method method = Method.GET, ArchiveHouse data = null)
        {
            var client = new RestClient(jsonBlobUrl);
            RestRequest request;
            if (string.IsNullOrEmpty(routeUrl))
            {
                request = new RestRequest();
            }
            else
            {
                request = new RestRequest(routeUrl);
            }
            IRestResponse response;
            request.Method = method;

            if (method != Method.GET && data != null)
            {
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(data);
                response = client.Execute(request);
            }
            else
            {
                response = client.Get(request);
            }

            return response;
        }

        //取得所有資料
        public ArchiveHouse GetAllDataFromDB(string jsonUrl = null)
        {
            if (jsonUrl == null)
                jsonUrl = Setting.GetJsonHostingUrlPath();//從config取得的值
            if (string.IsNullOrEmpty(jsonUrl))
            {
                var newUrl = initDefaultStruct();
                return GetAllDataFromDB(newUrl);
            }

            var raw = makeApiRequest(jsonUrl);
            var response =  JsonConvert.DeserializeObject<ArchiveHouse>(raw.Content);
            return response;
        }

        //如果還沒有json blob的url，就create一個，並回傳url path
        private string initDefaultStruct()
        {
            var data = new ArchiveHouse();
            var response = makeApiRequest("",Method.POST,data);
            string location = response.Headers
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault().ToString();
            var match = Regex.Match(location, ".*jsonBlob/(.+)");
            var token = match.Groups[1].Value;
            return token;
        }

        public void UpdateData(List<int> postIds)
        {
            var data = GetAllDataFromDB();
            foreach (var id in postIds)
            {
               data.houseList.Add(new houseObj(){postId = id,createTime = DateTime.Now}); 
            }
            //remove createTime is older than setting value
            var keepHour = Setting.GetDataKeepHour();
            data.houseList = data.houseList.Where(x => x.createTime.AddHours(keepHour) > DateTime.Now).ToList();

            makeApiRequest(Setting.GetJsonHostingUrlPath(), Method.PUT,data);
        }

    }
}
