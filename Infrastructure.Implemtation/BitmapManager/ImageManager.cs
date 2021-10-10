using Infrastructure.Interfaces.BitmapManager;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Infrastructure.Implemtation.BitmapManger
{
    public class ImageManager : IImageManager
    {
        public  Stream GlueImages(Stream firstImage, Stream secondImage)
        {
            var firstBitMap = new Bitmap(firstImage);
            var secondBitMap = new Bitmap(secondImage);

            firstBitMap = ResizeBitMap(firstBitMap, 375, 295);
            secondBitMap = ResizeBitMap(secondBitMap, 375, 295);


            var resultBitMap = new Bitmap(375, 590);

            using var graphics = Graphics.FromImage(resultBitMap);

            graphics.DrawImage(firstBitMap, 0, 0);
            graphics.DrawLine(new Pen(Brushes.White, 10), new Point(0, 295), new Point(375, 295));
            graphics.DrawImage(secondBitMap, 0, firstBitMap.Height);

            var memoryStream = new MemoryStream();


            resultBitMap.Save(memoryStream, ImageFormat.Jpeg);

            return memoryStream;
        }

        private static Bitmap ResizeBitMap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }
    }
}
