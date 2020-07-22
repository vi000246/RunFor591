using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.Entity
{
    //用來儲存match的物件
    public class ArchiveHouse
    {
        public Dictionary<DateTime, List<MatchHouse>> houseList { get; set; }
    }

    public class MatchHouse
    {
        public int houseId { get; set; }
        public bool IsNotify { get; set; }
    }
}
