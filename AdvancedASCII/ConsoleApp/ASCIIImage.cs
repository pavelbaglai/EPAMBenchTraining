using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    public class ASCIIImage
    {
        private const int ConsoleWidth = 150;
        private const int ConsoleHeight = 45;
        private static char[] charsByDarkness = { '#', '@', 'X', 'L', 'I', ':', '.', ' ' };
        private readonly int charWidthInPixels;
        private readonly int charHeightInPixels;
        private ExtendedImage originalImage;
        private int darkestColorValue = byte.MinValue;
        private int lightestColorValue = byte.MaxValue * 3;
        private int[][] originalImageDarkness;

        public static ASCIIImage CreateASCIIImage(ExtendedImage image)
        {
            return new ASCIIImage(image);
        }

        private ASCIIImage(ExtendedImage image)
        {
            originalImage = image;
            charWidthInPixels = image.Width / ConsoleWidth;
            charHeightInPixels = image.Height / ConsoleHeight;
            var rowCount = GetArrayDimension(image.Height, charHeightInPixels);
            originalImageDarkness = new int[rowCount][];
            ProcessOriginalImage();
        }

        public string GetASCIIImageAsString()
        {
            StringBuilder s = new StringBuilder();
            foreach (var darknessInRows in originalImageDarkness)
            {
                foreach (var darknessValue in darknessInRows)
                {
                    s.Append(GetCharacterRepresentation(darknessValue));
                }

                s.Append(Environment.NewLine);
            }

            return s.ToString();
        }

        private static int GetArrayDimension(int imageDimension, int consoleDimension)
        {
            return (int)Math.Ceiling((double)imageDimension / (double)consoleDimension);
        }

        private char GetCharacterRepresentation(int darknessValue)
        {
            return charsByDarkness[(darknessValue - lightestColorValue) * charsByDarkness.Length / (darkestColorValue - lightestColorValue + 1)];
        }

        private void ProcessOriginalImage()
        {
            for (int i = 0; i < originalImageDarkness.Length; i++)
            {
                var columnCount = GetArrayDimension(originalImage.Width, charWidthInPixels);
                var columns = new int[columnCount];
                for (int j = 0; j < columns.Length; j++)
                {
                    var averageDarkness = GetAverageDarknessOfImagePart(j * charWidthInPixels, i * charHeightInPixels);
                    columns[j] = averageDarkness;
                    SetDarknessBoundaries(averageDarkness);
                }

                originalImageDarkness[i] = columns;
            }
        }

        private void SetDarknessBoundaries(int darknessValue)
        {
            darkestColorValue = Math.Max(darkestColorValue, darknessValue);
            lightestColorValue = Math.Min(lightestColorValue, darknessValue);
        }

        private int GetAverageDarknessOfImagePart(int startXCoord, int startYCoord)
        {
            int totalDarknessValue = 0;
            int validPixelCount = 0;
            for (int y = startYCoord; y < startYCoord + charHeightInPixels; y++)
            {
                for (int x = startXCoord; x < startXCoord + charWidthInPixels; x++)
                {
                    if (IsValidPixelInImage(x, y))
                    {
                        totalDarknessValue = totalDarknessValue + originalImage.GetDarknessValue(x, y);
                        validPixelCount++;
                    }
                }
            }

            return totalDarknessValue / validPixelCount;
        }

        private bool IsValidPixelInImage(int x, int y)
        {
            return x < originalImage.Width && y < originalImage.Height;
        }

        /*private int GetAverageDarknessOfImagePart(int startXCoord, int startYCoord)
        {
            return originalImage.GetDarknessValue(startXCoord, startYCoord);
        }*/
    }
}
