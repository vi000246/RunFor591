using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using RunFor591.CrawlerUtility;
using SimpleServices;

namespace RunFor591
{
    public class RunFor591Service : IWindowsService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ApplicationContext AppContext { get; set; }

        public void Start(string[] args)
        {
            new Crawler().StartCrawl591();
            var MyTimer = new Timer();
            MyTimer.Elapsed += new ElapsedEventHandler(StartCrawler);
            var interval = Setting.GetTimerInterval();
            MyTimer.Interval = interval * 1000;
            MyTimer.Start();
        }
        private void StartCrawler(object sender, ElapsedEventArgs e)

        {
            new Crawler().StartCrawl591();
            log.Debug("crawler excute");
        }


        public void Stop()
        {
            AppContext.Log("Service stop!");
            log.Debug("Application Stopped");
        }
    }
}
