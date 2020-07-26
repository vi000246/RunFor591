using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RunFor591.NotifyUtility;

namespace RunFor591.Common
{
    public class AutoFacUtility
    {
        public static IContainer Container { get; set; }
        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LineNotify>().As<INotify>();  
            builder.RegisterType<JsonBlob>().As<IDataBase>();
            Container = builder.Build();
        }
    }

}
