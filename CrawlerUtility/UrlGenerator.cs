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

            //先組出base condition
            PropertyInfo[] properties = typeof(BaseCondition).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var settingValue = GetPropValue(filter.baseCondition,property.Name);
                if(settingValue != null && !string.IsNullOrEmpty(settingValue.ToString()))
                    returnParams.Add(String.Format("{0}={1}", property.Name, settingValue));
            }
            
            //依據地區搜尋條件，組出多組query string
            var querys = GenerateUrlBySearchModel(filter.regionCondition);

            var baseConditionQueryString = String.Join("&", returnParams.ToArray());
            foreach (var query in querys)
            {
                yield return "?" + query + baseConditionQueryString;
            }
            
        }

        //如果該region底下沒東西，也要組出url  
        //如果有選捷運，依據捷運線所屬哪個捷運，組出mrt參數(對應到region 桃捷=6 北捷=1 高雄捷運=17)
        private List<string> GenerateUrlBySearchModel(RegionCondition condition)
        {
            var conditionQueryString = new List<string>();
            //取出region跟mrt對照表
            //依據region，groupy by底下的捷運線跟鄉鎮

            //loop每個縣市
            foreach (var region in condition.Region)
            {
                //loop每個租屋類型
                foreach (var kind in condition.kind)
                {
                    foreach (var section in condition.Section)
                    {
                       //region + kind + section 
                    }

                    foreach (var matline in condition.mrtline)
                    {
                        foreach (var mrtStation in condition.mrtcoods)
                        {
                            //region + kind + marline + mrtStation
                        }
                    }

                    if (condition.Section.Count == 0 && condition.mrtline.Count == 0)
                    {
                        //組出region的url region + kind
                    }
                }
            }

            return conditionQueryString;
        }

        private void ValidateFilterCondition(SearchModel filter)
        {
            //驗證region condition
                //判斷是否有選擇region
                //判斷是否有選擇kind
                //判斷各參數是否列在mrt.json跟 region.json裡
                //如果有選擇鄉鎮section，判斷是否在region底下
                //如果有選擇捷運線，判斷是否在region底下
                //如果有選擇捷運線，捷運站為必選
                //判斷捷運站是否在捷運線底下
            //驗證base condition
            //各參數的格式是否正確
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

    }
}
