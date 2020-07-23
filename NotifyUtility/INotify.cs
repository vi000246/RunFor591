using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.NotifyUtility
{
    public interface INotify
    {
        void PubMessage(string msg);
    }
}
