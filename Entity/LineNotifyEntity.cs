using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.Entity
{
    public class LineNotifyEntity
    {
        //1000 characters max
        public string message { get; set; }
        //HTTP/HTTPS UR  Maximum size of 240×240px JPEG
        public string imageThumbnail { get; set; }
        //HTTP/HTTPS UR  Maximum size of 2048×2048px JPEG
        public string imageFullsize { get; set; }

    }
}
