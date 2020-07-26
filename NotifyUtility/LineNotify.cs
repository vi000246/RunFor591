using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using RestSharp;
using RunFor591.Common;
using RunFor591.CrawlerUtility;
using RunFor591.Entity;
using RunFor591.NotifyUtility;

namespace RunFor591
{
    public class LineNotify :INotify
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //註:api 一小時只能呼叫1000次
        private void LineNotifyApi(LineNotifyEntity form,byte[] imageByte = null)
        {
            var linetoken = Setting.GetLineToken();
            var client = new RestClient("https://notify-api.line.me/api/notify");
            var request = new RestRequest();
            request.Method = Method.POST;
            if (imageByte == null)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            }
            else
            {
                request.AddHeader("Content-Type", "multipart/form-data");

            }

            request.AddHeader("Authorization", $"Bearer {linetoken}");

           
            request.AddObject(form);
            if (imageByte != null)
            {
                request.AddFile("imageFile", imageByte, "img.jpeg", "image/jpeg");
            }


            var RawResponse = client.Execute(request);
            var res = JsonConvert.DeserializeObject<LineNotifyResponse>(RawResponse.Content);
            if (res.status != 200)
            {
                log.Error($"Send Line Notify error status:{res.status} msg:{res.message}");
            }
        }
        //發送訊息到line
        public void PubNotify(houseEntity house, PhotoListResponse photos)
        {
            var form = new LineNotifyEntity();
            form.message = Helper.NotifyMessageBuilder(house);
            var imageByte = ImageProcessor.ConvertMultipleImageIntoOne(photos);
            LineNotifyApi(form, imageByte);
        }
    }
}
