using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonConfig;
using Newtonsoft.Json;
using RunFor591.Entity;

namespace RunFor591
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
                throw ex;
            }
        }

        //取得json db的key
        public static string GetJsonBinKey()
        {
            return "";
        }

        //取得house filter的條件
        public static SearchModel GetFilterCondition()
        {
            return CastSettingToClass<SearchModel>("SearchModel");
        }

        private static T CastSettingToClass<T>(string key)
        {
            object json;
            
            if (((ConfigObject) Config.User).TryGetValue(key, out json))
            {
                return JsonConvert.DeserializeObject<T>(json.ToString()); 
            }
            else
            {
                log.Debug("Cannot CastSettingToClass T:" + nameof(T));
                return default(T);
            }
        }

        //取得要通知的line的key
    }
}
