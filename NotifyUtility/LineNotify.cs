using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineBotSDK.LIFF;
using LineBotSDK.Struct;
using LineBotSDK.Struct.Messages;
using LineBotSDK.Utility;
using RunFor591.NotifyUtility;

namespace RunFor591
{
    public class LineNotify :INotify
    {
        //發送訊息到line
        public void PubMessage(string msg)
        {
            var linetoken = Setting.GetLineToken();
            string token = linetoken.clientSecret; // please input the line bot token
            string userid = linetoken.clientId;// please input the user id
            string image_url = "https://i0.wp.com/blog.patw.me/wp-content/uploads/2017/05/ZcNMMLg.png?fit=800%2C416&ssl=1";
            string mp4_url = "https://www.legacyvet.com/sites/default/files/videos/Video%20%281%29.mp4";

            // --------- Send Message ---------
            // Send the text message to user
            MessageUtility.PushTextMessage(token, userid, "Hi");

            // Send the image message to user
            MessageUtility.PushImageMessage(token, userid, image_url, image_url);

            // Send the video message to user
            MessageUtility.PushVideoMessage(token, userid, mp4_url, image_url);

            // Send the sticker message to user
            MessageUtility.PushStickerMessage(token, userid, "1", "1");

            // Send the audio message to user, but I don't find the audio file
            //MessageUtility.PushAudioMessage();

            // Send the location message to user
            MessageUtility.PushLocationMessage(token, userid, "hi", "address:123", (decimal)35.65910807942215, (decimal)139.70372892916203);

            // Send the imagemap message to user
            Size size = new Size(800, 416);
            var actions = new List<IImagemapAction>();
            actions.Add(new ImagemapMessageAction("text", new ImagemapArea(0, 0, 400, 416)));
            actions.Add(new ImagemapURIAction("https://www.google.com/", new ImagemapArea(400, 0, 400, 416)));
            MessageUtility.PushImagemapMessage(token, userid, image_url, "altText", size, actions);

            // --------- Control the LIFF --------- 
            string url = "https://24h.pchome.com.tw/";
            string url2 = "https://www.google.com.tw/";

            LiffControl liffControl = new LiffControl(token);
            // Adds an app to LIFF
            var res = liffControl.AddingLIFFApp(LineBotSDK.LIFF.Struct.SizeOfLIFF.compact, url);

            // Updates LIFF app settings.
            liffControl.UpdateLIFFApp(res.liffId, LineBotSDK.LIFF.Struct.SizeOfLIFF.full, url2);

            // Gets information on all the LIFF apps registered in the channel.
            var apps = liffControl.GetAllLIFFApps();

            // Deletes a LIFF app.
            liffControl.DeleteLIFFApp(res.liffId);

            // Deletes all LIFF apps.
            liffControl.DeleteAllLIFFApps();
            Console.WriteLine("Hellow world");
        }
    }
}
