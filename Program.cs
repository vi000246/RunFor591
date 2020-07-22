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
        /// <summary>
        /// 應用程式的主要進入點。
        /// 如果要用console開啟，需要在專案的屬性頁面設定output type = 主控台應用
        /// 使用simpleService lib，要安裝請參考教學
        /// </summary>
        private static void Main(string[] args)
        {
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
    }
}
