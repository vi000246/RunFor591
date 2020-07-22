using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonConfig;

namespace RunFor591
{
    public static class Setting
    {
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
        //取得house filter的條件
        public static dynamic GetFilterCondition()
        {
            return Config.User.FilterCondition;
        }

        //取得要通知的line的key
    }
}
