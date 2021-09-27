using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhattanSkyline
{
    public class FirstImplementation
    {
        private int[] _input;
        public FirstImplementation(int[] input)
        {
            _input = input;
        }

        public int CalculateWater()
        {
            var result = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                var maxLeft = 0;
                var maxRight = 0;
                var currentValue = _input[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (_input[j] > maxLeft)
                        maxLeft = _input[j];
                }
                for (int j = i + 1; j < _input.Length; j++)
                {
                    if (_input[j] > maxRight)
                        maxRight = _input[j];
                }
                if (maxLeft > currentValue && maxRight > currentValue)
                    result += Math.Min(maxLeft, maxRight) - currentValue;
            }
            return result;
        }
    }
}
