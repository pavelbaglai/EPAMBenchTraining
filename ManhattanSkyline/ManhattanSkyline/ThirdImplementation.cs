using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhattanSkyline
{
    class ThirdImplementation
    {
        private int[] _input;

        public ThirdImplementation(int[] input)
        {
            _input = input;
        }

        public int CalculateWater()
        {
            var result = 0;
            int leftMax = 0, rightMax = 0;
            int lo = 0, hi = _input.Length - 1;

            
            while (lo <= hi)
            {
                if (_input[lo] < _input[hi])
                {
                    if (_input[lo] > leftMax)
                        leftMax = _input[lo];
                    else
                    {
                        result += leftMax - _input[lo];
                    }
                    lo++;
                }
                else
                {
                    if (_input[hi] > rightMax)
                        rightMax = _input[hi];
                    else
                    {
                        result += rightMax - _input[hi];
                    }
                    hi--;
                }
            }

            return result;
        }
    }
}
