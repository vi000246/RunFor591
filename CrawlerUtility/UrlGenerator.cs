using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using JsonConfig;
using RunFor591.Common;
using RunFor591.DataBase;
using RunFor591.Entity;
using RunFor591.LocationEntity;

namespace RunFor591.CrawlerUtility
{
    public class UrlGenerator
    {
        public static string _BaseUrl = "https://rent.591.com.tw/";
        public static string _HouseListUrl = "home/search/rsList";


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
                yield return "?" + query +"&"+ baseConditionQueryString;
            }
            
        }

        //如果該region底下沒東西，也要組出url  
        //如果有選捷運，依據捷運線所屬哪個捷運，組出mrt參數(對應到region 桃捷=6 北捷=1 高雄捷運=17)
        public List<string> GenerateUrlBySearchModel(RegionCondition regionCondition)
        {
            var conditionQueryString = new List<string>();
            
            
            //依據region，將搜尋條件group by
            var conditionList = ConvertFilterConditionToRegionList(regionCondition);

            //loop每個縣市
            foreach (var region in conditionList)
            {
                var regionQs = $"region={region.id}";
                //loop每個租屋類型
                foreach (var kind in regionCondition.kind)
                {
                    var kindQs = $"kind={kind}";
                    if (region.section.Any())
                    {
                        var sectionQs = regionQs + "&" + kindQs + "&"+
                                        $"section={String.Join(",", region.section.Select(x => x.id))}";
                        conditionQueryString.Add(sectionQs);
                    }

                        var mrtQs = $"mrt={region.mrt}";
                        foreach (var mrtline in region.mrt.mrtline)
                        {
                            var mrtlineQs = $"mrtline={mrtline.sid}";
                            var mrtStaionQs = regionQs + "&" + kindQs + "&" + mrtQs + "&" + mrtlineQs+ "&" +
                                              $"section={String.Join(",", mrtline.station.Select(x => x.nid))}";
                            conditionQueryString.Add(mrtStaionQs);
                        }
                        
                    
            
                    if (region.section.Count == 0 && region.mrt.mrtline.Count == 0)
                    {
                        //組出region的url region + kind
                        conditionQueryString.Add(regionQs+"&"+kindQs);
                    }
                }
            }

            return conditionQueryString;
        }

        public List<Region> ConvertFilterConditionToRegionList(RegionCondition condition)
        {
            //取出region跟mrt對照表
            var locationEntity = LocationJson.GetInstance();
            if(condition.Region.Count ==0)
                throw new ArgumentException("Please choose Region in settings.conf");
            //取出所選擇的縣市
            var regionList = locationEntity.regionEntity.region
                .Where(x => condition.Region.Contains(x.id))
                .ToList()
                .DeepClone<List<Region>>();


            for (int i = regionList.Count -1;i>=0;i--)
            {
                var currentRegion = regionList[i];
                //如果搜尋條件有指定此縣市底下的鄉鎮
                if (condition.Section.Intersect(currentRegion.section.Select(x => x.id)).Any())
                {
                    //移除其他沒被指定到的鄉鎮
                    currentRegion.section.RemoveAll(x => !condition.Section.Contains(x.id));
                }
                else
                {
                    //如果沒指定代表全選，刪掉所有鄉鎮，這樣才不會loop到
                    currentRegion.section.Clear();
                }

                //如果這縣市有捷運 再做捷運站判斷
                if (currentRegion.mrt != null)
                {
                    if (condition.mrtline.Intersect(currentRegion.mrt.mrtline.Select(x => x.sid)).Any())
                    {
                        //刪除沒被選到的捷運線
                        currentRegion.mrt.mrtline.RemoveAll(x => !condition.mrtline.Contains(x.sid));

                        //loop每個捷運線
                        for (int k = currentRegion.mrt.mrtline.Count - 1; k >= 0; k--)
                        {
                            var currentMrtLine = currentRegion.mrt.mrtline[k];
                            //依照validate規則，有選捷運線就一定要選捷運站
                            if (condition.mrtcoods.Intersect(currentMrtLine.station.Select(x => x.nid)).Any())
                            {
                                currentMrtLine.station.RemoveAll(x => !condition.mrtcoods.Contains(x.nid));

                            }
                            else
                            {
                                throw new ArgumentException(
                                    "Please choose mrt mrtcoods under mrtline in settings.conf.");
                            }
                        }
                    }
                    else
                    {
                        //刪除沒被選到的捷運
                        currentRegion.mrt = null;
                    }
                }


            }
            return regionList;
        }

        public void ValidateFilterCondition(SearchModel filter)
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
