using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Autofac;
using RunFor591.Common;
using RunFor591.CrawlerUtility;
using RunFor591.NotifyUtility;
using SimpleServices;

namespace RunFor591
{
    public class RunFor591Service : IWindowsService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ApplicationContext AppContext { get; set; }

        public void Start(string[] args)
        {
            //註冊IOC
            AutoFacUtility.Run();
            log.Info("Service Start.");
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            var MyTimer = new Timer();
            MyTimer.Elapsed += new ElapsedEventHandler(StartCrawler);
            var interval = Setting.GetTimerInterval();
            MyTimer.Interval = interval * 1000;
            MyTimer.Start();
        }
        private void StartCrawler(object sender, ElapsedEventArgs e)

        {
            try
            {
                log.Info("crawler excute");
                AppContext.Log("crawler excute");
                new Crawler().StartCrawl591();
            }
            catch (InvalidSettingException ex)
            {
                var msg = "Invalid Setting" + ex.Message;
                log.Error(msg);
                AppContext.Log(msg);
                Helper.ShowMessageBox(msg,"Invalid settings.conf");
                //如果是windows service，就用serviceController停止，service name寫在program.cs
                if(!Environment.UserInteractive)
                    new ServiceController("RunFor591").Stop();
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                var msg = "Service Error. msg:" + ex.Message+ex.StackTrace;
                AppContext.Log(msg);
                log.Error(msg);
                var notifyService = AutoFacUtility.Container.Resolve<INotify>();
                notifyService.SendMessage(msg);
            }
        }


        public void Stop()
        {
            AppContext.Log("Service stop!");
            log.Info("Service stop!");
        }

        void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            var msg = "Windows Service Error on: {0} " + e.Message + e.StackTrace;
            log.Error(msg);
            var notifyService = AutoFacUtility.Container.Resolve<INotify>();
            notifyService.SendMessage(msg);
        }
    }
}
