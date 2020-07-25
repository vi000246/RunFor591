using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.Entity
{
    public class photos
    {
        public List<string> thumbs { get; set; }
        public List<string> large { get; set; }
        public string cover { get; set; }

    }

    public class PhotoListResponse
    {
        public int status { get; set; }
        public photos data { get; set; }

    }

}
