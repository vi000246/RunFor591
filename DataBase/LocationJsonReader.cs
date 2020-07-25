using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RunFor591.LocationEntity;

namespace RunFor591.DataBase
{
    public class LocationJson
    {
        private static readonly object thisLock = new object();
        //此物件包含縣市、鄉鎮、捷運
        public RegionEntity regionEntity { get; set; }
        // 將唯一實例設為 private static
        private static LocationJson instance;


        private LocationJson()
        {
            var mrts = GetMRTList();
            regionEntity = GetRegionEntity();
            //把捷運塞到對應的縣市底下
            regionEntity.region = regionEntity.region.Select(x => new
            Region{
                id = x.id,
                txt = x.txt,
                section = x.section,
                mrts = mrts.mrts.Where(m => m.mrt == x.id).ToList()
            }).ToList();
        }

        // 外界只能使用靜態方法取得實例
        public static LocationJson GetInstance()
        {
            if (null == instance)
            {
                lock (thisLock)
                {
                    if (null == instance)
                    {
                        instance = new LocationJson();
                    }
                }
            }

            return instance;
        }

        public static T LoadJson<T>(string fileName)
        {
            T items;
            using (StreamReader r = new StreamReader(fileName, System.Text.Encoding.Default))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<T>(json);
            }

            return items;
        }

        private MRTEntity GetMRTList()
        {
            return LoadJson<MRTEntity>("LocationEntity\\MRT.json");
        }

        private RegionEntity GetRegionEntity()
        {
            return LoadJson<RegionEntity>("LocationEntity\\Region.json");
        }

    }
}
