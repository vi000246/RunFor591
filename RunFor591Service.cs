using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SimpleServices;

namespace RunFor591
{
    public class RunFor591Service : IWindowsService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ApplicationContext AppContext { get; set; }

        public void Start(string[] args)
        {
            var MyTimer = new Timer();
            MyTimer.Elapsed += new ElapsedEventHandler(MyTimer_Elapsed);
            var minute = (float)1/60;//每N分鐘執行一次
            MyTimer.Interval = (int)(minute * 60) * 1000;
            MyTimer.Start();
        }
        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)

        {
            AppContext.Log("service run.");
            log.Debug("Application Excute");
        }
        public void Stop()
        {
            AppContext.Log("Service stop!");
            log.Debug("Application Stopped");
        }
    }
}
