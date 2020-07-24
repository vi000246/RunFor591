using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.LocationEntity
{
    public class MRTEntity
    {
        public List<Mrt> mrts { get; set; }

    }
    public class Mrt
    {
        //捷運代號，對應到region  北捷=1 高捷=17 桃捷=6
        public string mrt { get; set; }
        //捷運名稱
        public string name { get; set; }
        //捷運線 ex.紅線、橘線、文湖線
        public List<Mrtline> mrtline { get; set; }

    }
    public class Mrtline
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string name { get; set; }
        public string sid { get; set; }
        public string zoom { get; set; }
        //捷運站
        public List<Station> station { get; set; }

    }

    public class Station
    {
        public string sid { get; set; }
        public string name { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string zoom { get; set; }
        public string nid { get; set; }

    }








}
