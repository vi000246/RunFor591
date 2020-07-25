using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunFor591.Entity;

namespace RunFor591.NotifyUtility
{
    public interface INotify
    {
        void PubNotify(houseEntity house,PhotoListResponse photos);
    }
}
