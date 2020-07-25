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
        public string id { get; set; }
        public string txt { get; set; }
        //鄉鎮
        public List<Section> section { get; set; }
        public Mrt mrt { get; set; }//每個縣市只有一個捷運公司(目前只有北捷，高捷，桃捷)

    }
}
