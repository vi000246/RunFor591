using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.LocationEntity
{
    public class MRTEntity
    {
        public TaipeiMrt taipei_mrt { get; set; }
        public KaohsiungMrt kaohsiung_mrt { get; set; }
        public TaoyuanMrt taoyuan_mrt { get; set; }

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

    public class Mrtline
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string name { get; set; }
        public string sid { get; set; }
        public string zoom { get; set; }
        public List<Station> station { get; set; }

    }

    public class TaipeiMrt
    {
        public string mrt { get; set; }
        public List<Mrtline> mrtline { get; set; }

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

    public class Mrtline2
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string name { get; set; }
        public string sid { get; set; }
        public string zoom { get; set; }
        public List<Station2> station { get; set; }

    }

    public class KaohsiungMrt
    {
        public string mrt { get; set; }
        public List<Mrtline2> mrtline { get; set; }

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

    public class Mrtline3
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string name { get; set; }
        public string sid { get; set; }
        public string zoom { get; set; }
        public List<Station3> station { get; set; }

    }

    public class TaoyuanMrt
    {
        public string mrt { get; set; }
        public List<Mrtline3> mrtline { get; set; }

    }




}
