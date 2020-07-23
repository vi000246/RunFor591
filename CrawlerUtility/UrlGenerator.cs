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

        //用來取得csrf token跟cookie
        public string GetEntryUrl()
        {
            return UrlGenerator._BaseUrl + QueryStringBuilder().ToList().FirstOrDefault();
        }

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
            PropertyInfo[] properties = typeof(SearchModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var settingValue = GetPropValue(filter,property.Name);
                //kind是用來判斷租屋類型 1:整層住家 2:獨立套房 3:分租套房 這個需要分開request
                if (property.Name != "Kind")
                {
                    returnParams.Add(String.Format("{0}={1}", property.Name, settingValue));
                }
            }

            var qs = String.Join("&", returnParams.ToArray());
            foreach (var kind in filter.Kind)
            {
                yield return "?kind=" + kind + qs;
            }
            
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

    }
}
