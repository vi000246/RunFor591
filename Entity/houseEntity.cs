using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.Entity
{
    public class houseEntity
    {
        public string title { get; set; }
        public string address { get; set; }
        //門牌號碼
        public string addr_number_name { get; set; }
        //house url
        public string houseUrl
        {
            get { return $"https://rent.591.com.tw/rent-detail-{this.post_id}.html"; }
        }

        //街道名稱
        public string street_name { get; set; }
        //巷道
        public string alley_name { get; set; }
        //封面照片
        public string coverUrl { get; set; }
        public List<string> album { get; set; }
        //距離 (以後串接gmap api用)
        public int distance { get; set; }
        //樓層
        public string floorInfo { get; set; }
        //房屋類型名稱 ex.獨立套房、雅房
        public string kind_name { get; set; }
        //文章編號
        public int post_id { get; set; }
        //價格
        public string price { get; set; }
        //格局
        public string layout { get; set; }

        //縣市名稱
        public string regionName { get; set; }
        //地區
        public string sectionname { get; set; }
        //更新時間
        public DateTime updateTime { get; set; }
        //跟上一個不知差在哪裡
        public DateTime refreshTime { get; set; }
        public bool isNew { get; set; }
    }
}
