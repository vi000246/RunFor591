using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunFor591.Entity;

namespace RunFor591
{
    interface IDataBase
    {

        ArchiveHouse GetAllDataFromDB(string jsonUrl = null);
        void UpdateData(List<int> postIds);
    }
}
