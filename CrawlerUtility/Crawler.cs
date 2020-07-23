using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace RunFor591
{
    public class Crawler
    {

        //需要loop取得整層住家/套房/獨立套房的data
        public string GetRawContext()
        {
            var client = new RestClient("https://api.twitter.com/1.1");
            client.Authenticator = new HttpBasicAuthenticator("username", "password");

            var request = new RestRequest("statuses/home_timeline.json", DataFormat.Json);

            var response = client.Get(request);
            return response.Content;
        }

        public void ConvertRawDataToObj()
        {

        }

        public void FilterHouse()
        {

        }
    }
}
