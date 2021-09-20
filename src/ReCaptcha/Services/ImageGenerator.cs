using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

namespace ReCaptcha.Services
{
    internal class ImageGenerator : IImageGenerator
    {
        public byte[] GetBytes(string data)
        {
            Bitmap photo = new Bitmap(100, 75);
            Graphics pointer = Graphics.FromImage(photo);
            HatchBrush backBrush = new HatchBrush(HatchStyle.Cross, Color.DeepSkyBlue, Color.White);

            pointer.FillRectangle(backBrush, 0, 0, 100, 75);
            pointer.RotateTransform(new Random().Next(-15, 15));
            pointer.DrawString(data, new Font("arial", 24), Brushes.DarkBlue, new PointF(10, 20));
            pointer.Save();

            MemoryStream photoStream = new MemoryStream();
            photo.Save(photoStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return photoStream.ToArray();
        }
    }
    public interface IImageGenerator
    {
        byte[] GetBytes(string data);
    }

}
