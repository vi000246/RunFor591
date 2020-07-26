using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RunFor591.Entity;

namespace RunFor591.Common
{
    public static class ImageProcessor
    {
        public static byte[] ConvertMultipleImageIntoOne(PhotoListResponse data)
        {
            var images = ConvertUrlToImages(data);
            var oneImg = Merge(images);
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(oneImg, typeof(byte[]));
        }

        public static Image ReadImage(string imagePath)
        {
            RestClient restClient = new RestClient(imagePath);
            var fileBytes = restClient.DownloadData(new RestRequest(Method.GET));
            MemoryStream ms = new MemoryStream(fileBytes);
            Image img = Image.FromStream(ms);
            return img;
        }

        public static List<Image> ConvertUrlToImages(PhotoListResponse data)
        {
            var imageList = new List<Image>();
            imageList.Add(ReadImage(data.data.cover));
            foreach (var img in data.data.large)
            {
                imageList.Add(ReadImage(img));
            }

            return imageList;
        }

        private static Bitmap Merge(List<Image> images)
        {
            var width = 0;
            var height = 0;
            // Get max width and height of the image
            foreach (var image in images)
            {
                width = image.Width > width ? image.Width : width;
                height = image.Height > height ? image.Height : height;
            }
            // merge images
            var bitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bitmap))
            {
                foreach (var image in images)
                {
                    g.DrawImage(image, 0, 0);
                }
            }
            return bitmap;
        }
    }
}
