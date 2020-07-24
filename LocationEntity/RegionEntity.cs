using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.LocationEntity
{
    public class RegionEntity
    {
        //縣市
        public List<Region> region { get; set; }
    }
    public class Section
    {
        public string id { get; set; }
        public string name { get; set; }

    }

    public class Region
    {
        public int id { get; set; }
        public string txt { get; set; }
        //鄉鎮
        public List<Section> section { get; set; }

    }
}
