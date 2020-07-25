using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;
using RunFor591.CrawlerUtility;
using RunFor591.NotifyUtility;

namespace RunFor591
{
    public class LineNotify :INotify
    {
        private void LineNotifyApi()
        {
            var linetoken = Setting.GetLineToken();
            var client = new RestClient("https://notify-api.line.me/api/notify");
            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Bearer {linetoken}");
            var form = new {message = "from vs"};
            request.AddObject(form);


            var response = client.Execute(request).Content;
        }
        //發送訊息到line
        public void PubMessage(string msg)
        {
            LineNotifyApi();
        }
    }
}
