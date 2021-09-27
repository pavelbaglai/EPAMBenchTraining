using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhattanSkyline
{
    public static class RandomSkylineGenerator
    {
        public static int[] GenerateSkyline(int inputLength, int minBuildingHeight, int maxBuildingHeight)
        {
            var input = new int[inputLength];
            Random rnd = new Random();
            for (int i = 0; i < inputLength; i++)
            {
                input[i] = rnd.Next(minBuildingHeight, maxBuildingHeight);
            }
            return input;
        }
    }
}
