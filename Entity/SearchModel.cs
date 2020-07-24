using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.Entity
{
    public class SearchModel
    {
        public RegionCondition regionCondition { get; set; }
       
        public BaseCondition baseCondition { get; set; }

    }

    public class RegionCondition
    {
        public List<string> kind { get; set; }
        public List<string> Region { get; set; }
        public List<string> Section { get; set; }
        public List<string> mrtline { get; set; }
        public List<string> mrtcoods { get; set; }
    }

    public class BaseCondition
    {
        public string RendPrice { get; set; }
        public string Area { get; set; }
        public string Order { get; set; }
        public string OrderType { get; set; }
        public string HasImg { get; set; }
        public string Role { get; set; }
        public string Pattern { get; set; }
        public string NotCover { get; set; }
        public string Floor { get; set; }
        public string Option { get; set; }
        public string FirstRow { get; set; }

    }




}
