using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using log4net;
using RunFor591.DataBase;

namespace RunFor591.Common
{
    public static class Utility
    {
        //依據名稱，取得region或section的id
        public static string GetLocationIdByName(string name)
        {
            var locationEntity = LocationJson.GetInstance();
            var region = locationEntity.regionEntity.region.FirstOrDefault(x=>x.txt==name);
            if (region != null)
                return region.id;
            foreach (var reg in locationEntity.regionEntity.region)
            {
                foreach (var section in reg.section)
                {
                    if (section.name == name)
                        return section.id;
                } 
            }
            throw new ArgumentException("Cannot find name in Region.json  name:"+name);
        }

        //依據捷運線或捷運站名稱，取得id
        public static string GetMrtIdByName(string name)
        {
            var locationEntity = LocationJson.GetInstance();
            foreach (var mrt in locationEntity.mrtEntity.mrts)
            {

                foreach (var mrtline in mrt.mrtline)
                {
                    if (mrtline.name == name)
                        return mrtline.sid;
                    foreach (var station in mrtline.station)
                    {
                        if (station.name == name)
                            return station.nid;
                    }
                }
            }
            throw new ArgumentException("Cannot find name in MrtRegionId.json name:"+name);

            
        }
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static void WriteMultipleLineLig(string msgTitle,List<string> msgs ,ILog log)
        {
            log.Info($"========== {msgTitle} ============");
            foreach (var msg in msgs)
            {
                log.Info(msg);
            }
            log.Info("=========================================");
        }

    }
}
