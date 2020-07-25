using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;
using RunFor591.CrawlerUtility;
using RunFor591.Entity;
using RunFor591.NotifyUtility;

namespace RunFor591
{
    public class LineNotify :INotify
    {
        private void LineNotifyApi(LineNotifyEntity form)
        {
            var linetoken = Setting.GetLineToken();
            var client = new RestClient("https://notify-api.line.me/api/notify");
            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Bearer {linetoken}");
           
            request.AddObject(form);
            if(form.imageFile != null)
                request.AddParameter("application/octet-stream", form.imageFile, ParameterType.RequestBody);


            var response = client.Execute(request).Content;
        }
        //發送訊息到line
        public void PubNotify(houseEntity house, PhotoListResponse photos)
        {
            var form = new LineNotifyEntity();
            form.message = "test call api";
            form.imageThumbnail = "https://i.imgur.com/tmdLbuo.jpg";
            form.imageFullsize = "https://i.imgur.com/tmdLbuo.jpg";
            LineNotifyApi(form);
        }
    }
}
