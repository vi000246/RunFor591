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
       public List<houseObj> houseList { get; set; } = new List<houseObj>();
    }

    public class houseObj
    {
        public int postId { get; set; }
        public DateTime createTime { get; set; }
    }

}
