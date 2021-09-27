using System;
using System.Drawing;
using System.IO;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    public class ExtendedImage
    {
        private readonly Bitmap image;

        public static ExtendedImage CreateImage(string fileName)
        {
            return new ExtendedImage(fileName);
        }

        private ExtendedImage(string fileName)
        {
            image = LoadImageFromFile(fileName);
        }

        public int Width => image.Width;

        public int Height => image.Height;

        public int GetDarknessValue(int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            return pixel.R + pixel.G + pixel.B;
        }

        private Bitmap LoadImageFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"File not found: {fileName}", nameof(fileName));
            }

            using (var img = Image.FromFile(fileName))
            {
                return new Bitmap(img);
            }
        }
    }
}