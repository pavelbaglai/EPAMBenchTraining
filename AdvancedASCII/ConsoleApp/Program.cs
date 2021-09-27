using System;
using System.Collections.Generic;
using System.Drawing;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    internal class Program
    {
        private static string imagePath = "pair_hiking.png";

        public static void Main()
        {
            ExtendedImage image = ExtendedImage.CreateImage(imagePath);
            ASCIIImage asciiImage = ASCIIImage.CreateASCIIImage(image);
            Console.WriteLine(asciiImage.GetASCIIImageAsString());
            Console.ReadLine();
        }
    }
}
