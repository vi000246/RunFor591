using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using SimpleServices;

namespace RunFor591
{
    [RunInstaller(true)]
    public class Program : SimpleServiceApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        private static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            XmlConfigurator.Configure(new System.IO.FileInfo("./log4net.config"));
            new Service(args, new List<IWindowsService> {new RunFor591Service()}.ToArray
                        ,
                        installationSettings: (serviceInstaller, serviceProcessInstaller) =>
                        {
                            serviceInstaller.ServiceName = "RunFor591";
                            serviceInstaller.StartType = ServiceStartMode.Automatic;
                            serviceProcessInstaller.Account = ServiceAccount.LocalService;
                        },
                        configureContext: x => { x.Log = Console.WriteLine; })
                    .Host();
        }
        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            log.Error(e.ExceptionObject);
        }
    }
}
