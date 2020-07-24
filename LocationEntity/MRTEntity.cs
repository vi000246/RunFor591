using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.LocationEntity
{
    public class MRTEntity
    {
        public List<TaipeiMrt> taipei_mrt { get; set; }
        public List<KaohsiungMrt> kaohsiung_mrt { get; set; }
        public List<TaoyuanMrt> taoyuan_mrt { get; set; }

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

    public class TaipeiMrt
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string name { get; set; }
        public string sid { get; set; }
        public string zoom { get; set; }
        public List<Station> station { get; set; }

    }

    public class Station2
    {
        public string sid { get; set; }
        public string name { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string zoom { get; set; }
        public string nid { get; set; }

    }

    public class KaohsiungMrt
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string name { get; set; }
        public string sid { get; set; }
        public string zoom { get; set; }
        public List<Station2> station { get; set; }

    }

    public class Station3
    {
        public string sid { get; set; }
        public string name { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string zoom { get; set; }
        public string nid { get; set; }

    }

    public class TaoyuanMrt
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string name { get; set; }
        public string sid { get; set; }
        public string zoom { get; set; }
        public List<Station3> station { get; set; }

    }


}
