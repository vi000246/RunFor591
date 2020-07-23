using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.Entity
{
    public class ResponseHouseEntity
    {
        public int status { get; set; }
        public Data data { get; set; }
        public string records { get; set; }
        public int is_recom { get; set; }
        public IList<object> deal_recom { get; set; }
        public int online_social_user { get; set; }
        public BluekaiData bluekai_data { get; set; }
    }

    public class TopData
    {
        public int is_new_index { get; set; }
        public int is_new_list { get; set; }
        public int type { get; set; }
        public int post_id { get; set; }
        public int isAd { get; set; }
        public int regionid { get; set; }
        public int photoNum { get; set; }
        public string classLast { get; set; }
        public string detail_url { get; set; }
        public string address { get; set; }
        public string img_src { get; set; }
        public string alt { get; set; }
        public string address_2 { get; set; }
        public string section_str { get; set; }
        public int region { get; set; }
        public string kind_str { get; set; }
        public string area { get; set; }
        public string price_str { get; set; }
        public int recom_num { get; set; }
        public string address_3 { get; set; }
        public string ico { get; set; }
        public string price { get; set; }
        public string price_unit { get; set; }
        public string onepxImg { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public int post_id { get; set; }
        public int regionid { get; set; }
        public int sectionid { get; set; }
        public int streetid { get; set; }
        public int room { get; set; }
        public double area { get; set; }
        public string price { get; set; }
        public int storeprice { get; set; }
        public int comment_total { get; set; }
        public int comment_unread { get; set; }
        public int comment_ltime { get; set; }
        public int hasimg { get; set; }
        public int kind { get; set; }
        public int shape { get; set; }
        public int houseage { get; set; }
        public string posttime { get; set; }
        public int updatetime { get; set; }
        public int refreshtime { get; set; }
        public int checkstatus { get; set; }
        public string status { get; set; }
        public int closed { get; set; }
        public string living { get; set; }
        public string condition { get; set; }
        public int isvip { get; set; }
        public int mvip { get; set; }
        public int is_combine { get; set; }
        public string cover { get; set; }
        public int browsenum { get; set; }
        public int browsenum_all { get; set; }
        public int floor2 { get; set; }
        public int floor { get; set; }
        public string ltime { get; set; }
        public object cases_id { get; set; }
        public int social_house { get; set; }
        public int distance { get; set; }
        public string search_name { get; set; }
        public object mainarea { get; set; }
        public object balcony_area { get; set; }
        public object groundarea { get; set; }
        public string linkman { get; set; }
        public int housetype { get; set; }
        public string street_name { get; set; }
        public string alley_name { get; set; }
        public string lane_name { get; set; }
        public string addr_number_name { get; set; }
        public string kind_name_img { get; set; }
        public string address_img { get; set; }
        public string cases_name { get; set; }
        public string layout { get; set; }
        public string layout_str { get; set; }
        public int allfloor { get; set; }
        public string floorInfo { get; set; }
        public string house_img { get; set; }
        public string houseimg { get; set; }
        public string cartplace { get; set; }
        public string space_type_str { get; set; }
        public string photo_alt { get; set; }
        public int addition4 { get; set; }
        public int addition2 { get; set; }
        public int addition3 { get; set; }
        public string vipimg { get; set; }
        public string vipstyle { get; set; }
        public string vipBorder { get; set; }
        public int new_list_comment_total { get; set; }
        public string comment_class { get; set; }
        public string price_hide { get; set; }
        public string kind_name { get; set; }
        public object photoNum { get; set; }
        public string filename { get; set; }
        public string nick_name { get; set; }
        public string new_img { get; set; }
        public string regionname { get; set; }
        public string sectionname { get; set; }
        public string icon_name { get; set; }
        public string icon_class { get; set; }
        public string fulladdress { get; set; }
        public string address_img_title { get; set; }
        public string browsenum_name { get; set; }
        public string unit { get; set; }
        public int houseid { get; set; }
        public string region_name { get; set; }
        public string section_name { get; set; }
        public string addInfo { get; set; }
        public string onepxImg { get; set; }
    }

    public class Data
    {
        public IList<TopData> topData { get; set; }
        public IList<object> biddings { get; set; }
        public IList<Datum> data { get; set; }
        public string page { get; set; }
    }

    public class BluekaiData
    {
        public string region_id { get; set; }
        public string section_id { get; set; }
        public int sale_price { get; set; }
        public int rental_price { get; set; }
        public string unit_price_per_ping { get; set; }
        public string room { get; set; }
        public string shape { get; set; }
        public string mrt_city { get; set; }
        public string mrt_line { get; set; }
        public int tag { get; set; }
        public string type { get; set; }
        public string kind { get; set; }
        public string page { get; set; }
    }

}
