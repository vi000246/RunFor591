using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunFor591.LocationEntity;

namespace RunFor591.DataBase
{
    public class LocationJson
    {
        public MRTEntity mrtList { get; set; }
        public RegionEntity regionList { get; set; }

        public LocationJson()
        {
            mrtList = this.GetMRTList();
            regionList = this.GetRegionEntity();
        }

        public MRTEntity GetMRTList()
        {
            throw new NotImplementedException();
        }

        public RegionEntity GetRegionEntity()
        {
            throw new NotImplementedException();
        }

    }
}
