using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JsonConfig;
using RunFor591.Entity;

namespace RunFor591.CrawlerUtility
{
    public class UrlGenerator
    {
        public static string _BaseUrl = "https://rent.591.com.tw/";
        public static string _HouseListUrl = "home/search/rsList";

        //591列表頁面 用來取得csrf token跟cookie
        public string GetEntryUrl()
        {
            //用firstOrDefault 反正只是用來取得token跟cookie
            return UrlGenerator._BaseUrl + QueryStringBuilder().ToList().FirstOrDefault();
        }

        //依據搜尋條件，產生所有要request的api url
        public IEnumerable<string> GetHouseListApiUrl()
        {
            foreach (var queryString in QueryStringBuilder())
            {
                yield return UrlGenerator._HouseListUrl + queryString;
            }
            
        }

        private IEnumerable<string> QueryStringBuilder()
        {
            List<string> returnParams = new List<string>();
            var filter = Setting.GetFilterCondition();
            ValidateFilterCondition(filter);

            PropertyInfo[] properties = typeof(SearchModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var settingValue = GetPropValue(filter,property.Name);
                //kind是用來判斷租屋類型 1:整層住家 2:獨立套房 3:分租套房
                //region是縣市，kind跟region都不能多選,必須loop產出request url
                if (property.Name != "Kind" && property.Name != "Region")
                {
                    returnParams.Add(String.Format("{0}={1}", property.Name, settingValue));
                }
            }

            //loop縣市、鄉鎮、捷運
            //如果有選捷運，依據捷運線所屬哪個捷運，組出mrt參數(對應到region 桃捷=6 北捷=1 高雄捷運=17)
            //依據region，groupy by底下的捷運線跟鄉鎮

            var qs = String.Join("&", returnParams.ToArray());
            foreach (var kind in filter.Kind)
            {
                yield return "?kind=" + kind + qs;
            }
            
        }

        private void ValidateFilterCondition(SearchModel filter)
        {
            //判斷是否有選擇region
            //如果有選擇鄉鎮section，判斷是否在region底下
            //如果有選擇捷運線，判斷是否在region底下
            //如果有選擇捷運線，捷運站為必選
            //判斷捷運站是否在捷運線底下
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

    }
}
