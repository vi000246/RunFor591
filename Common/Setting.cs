using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonConfig;
using Newtonsoft.Json;
using RunFor591.Entity;

namespace RunFor591.Common
{
    //註:Config.User會讀取settings.conf的設定
    //其他使用方式請參考JsonConfig的說明頁面
    public static class Setting
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //取得程式執行區間，單位:秒
        public static int GetTimerInterval()
        {
            try
            {
                var min = Config.User.TimerIntervalMinute;
                var sec = Config.User.TimerIntervalSec;
                return (min * 60) + sec;
            }
            catch (Exception ex)
            {
                var msg = "Invalid setting, GetTimerInterval fail.Invalid setting. " + ex.Message;
                log.Debug(msg);
                throw new InvalidSettingException(msg);
            }
        }

        //取得json db的url位置
        public static string GetJsonHostingUrlPath()
        {
            return Config.User.JsonHostingUrlPath;
        }

        //設定已通知的房子的保留小時，預設48hr，因為591的new icon也只會存在24hr,就不會撈到重覆資料 type:int 
        public static int GetDataKeepHour()
        {
            return 48;
        }

        //暫不支援 會造成file lock
//        public static void SetJsonHostingUrlPath(string path)
//        {
//            Config.User.JsonHostingUrlPath = path;
//            var modifiedJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Config.User);
//            File.WriteAllText("settings.conf",modifiedJsonString);
//        }

        public static string GetLineToken()
        {
            var token =  Config.User.LineAccessToken;
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidSettingException("Invalid setting, Line  access token cannot be empty.");
            }
            return token;
        }


        //取得house filter的條件
        public static SearchModel GetFilterCondition()
        {
            return CastSettingToClass<SearchModel>("SearchModel");
        }

        private static T CastSettingToClass<T>(string key)
        {
            object json;

            try
            {
                if (((ConfigObject) Config.User).TryGetValue(key, out json))
                {
                    return JsonConvert.DeserializeObject<T>(json.ToString());
                }
                else
                {
                    var msg = "Cannot CastSettingToClass T:" + nameof(T);
                    log.Debug(msg);
                    throw new InvalidSettingException(msg);
                }
            }
            catch (Exception ex)
            {
                var msg = "cast setting fail. " + ex.Message;
                log.Debug(msg);
                throw new InvalidSettingException(msg);
            }
        }

    }
}
