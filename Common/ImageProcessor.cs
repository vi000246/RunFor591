﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RunFor591.Entity;
using Encoder = System.Text.Encoder;

namespace RunFor591.Common
{
    public static class ImageProcessor
    {
        public static byte[] ConvertMultipleImageIntoOne(PhotoListResponse data)
        {
            var images = ConvertUrlToImages(data);
            var oneImg = Merge(images);
            return SaveImageToByteArray(oneImg);
        }

        //壓縮圖片
        private static byte[] SaveImageToByteArray(Image image, int jpegQuality = 90)
        {
            using (var ms = new MemoryStream())
            {
                var jpegEncoder = GetEncoder(ImageFormat.Jpeg);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)jpegQuality);
                image.Save(ms, jpegEncoder, encoderParameters);
                return ms.ToArray();
            }
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
            try
            {
                if (!string.IsNullOrEmpty(data.data.cover))
                    imageList.Add(ReadImage(data.data.cover));
                if (data.data != null)
                {
                    foreach (var img in data.data.large)
                    {
                        imageList.Add(ReadImage(img));
                    }
                }
            }
            catch (Exception ex)
            {
                return imageList;
            }

            return imageList;
        }

        private static Bitmap Merge(List<Image> images)
        {
            var width = 0;
            var height = 0;

            //將圖片每三個一組
            int groupByEveryImageCount = 3;
            var imgGroup = images.Select((value, index) => new {PairNum = index / groupByEveryImageCount, value})
                .GroupBy(pair => pair.PairNum)
                .Select(grp => grp.Select(g => g.value));
            foreach (var group in imgGroup)
            {
                height += group.Max(x => x.Height);
                var currentTotalWidth = group.Sum(x => x.Width);
                width = currentTotalWidth > width ? currentTotalWidth : width;
            } 

            var bitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bitmap))
            {
                var localWidth = 0;
                var localHeight = 0;

                foreach (var group in imgGroup)
                {
                    localWidth = 0;
                    foreach (var image in group)
                    {
                        g.DrawImage(image, localWidth, localHeight);
                        localWidth += image.Width;
                    }
                    localHeight += group.Max(x=>x.Height);
                }
            } return bitmap;
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }
    }
}
