using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using log4net;
using RunFor591.DataBase;
using RunFor591.Entity;

namespace RunFor591.Common
{
    public static class Helper
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
            throw new InvalidSettingException("Cannot find name in Region.json  name:"+name);
        }
        public static string GetLocationIdById(string Id)
        {
            var locationEntity = LocationJson.GetInstance();
            var region = locationEntity.regionEntity.region.FirstOrDefault(x => x.id == Id);
            if (region != null)
                return region.id;
            foreach (var reg in locationEntity.regionEntity.region)
            {
                foreach (var section in reg.section)
                {
                    if (section.id == Id)
                        return section.id;
                }
            }
            throw new InvalidSettingException("Cannot find id in Region.json  id:" + Id);
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
            throw new InvalidSettingException("Cannot find name in MrtRegionId.json name:"+name);
        }

        public static string GetMrtIdById(string Id)
        {
            var locationEntity = LocationJson.GetInstance();
            foreach (var mrt in locationEntity.mrtEntity.mrts)
            {

                foreach (var mrtline in mrt.mrtline)
                {
                    if (mrtline.sid == Id)
                        return mrtline.sid;
                    foreach (var station in mrtline.station)
                    {
                        if (station.nid == Id)
                            return station.nid;
                    }
                }
            }
            throw new InvalidSettingException("Cannot find id in MrtRegionId.json id:" + Id);
        }

        public static string NotifyMessageBuilder(houseEntity house)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("===============\r\n");
            sb.Append("名稱:" + house.title+"\r\n");
            sb.Append("類型:" + house.kind_name+"\r\n");
            sb.Append(house.floorInfo+"\r\n");
            sb.Append("價格:" + house.price+"\r\n");
            sb.Append("地址:" + house.address + "\r\n");
            if(!string.IsNullOrEmpty(house.layout))
                sb.Append("格局:" + house.layout);
            sb.Append("網址:" + house.houseUrl);
            return sb.ToString();
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
        public static IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
        public static void ShowMessageBox(string message, string title)
        {
            int resp = 0;
            WTSSendMessage(
                WTS_CURRENT_SERVER_HANDLE,
                WTSGetActiveConsoleSessionId(),//獲得當前顯示的桌面所在的SessionID
                title, title.Length,
                message, message.Length,
                0, 0, out resp, false);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int WTSGetActiveConsoleSessionId();

        [DllImport("wtsapi32.dll", SetLastError = true)]
        public static extern bool WTSSendMessage( //一個Session中的進程可以用WTSSendMessage，讓另一個Session彈出對話框
            IntPtr hServer,
            int SessionId,
            String pTitle,
            int TitleLength,
            String pMessage,
            int MessageLength,
            int Style,
            int Timeout,
            out int pResponse,
            bool bWait);

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
