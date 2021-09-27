using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhattanSkyline
{
    public class SecondImplementation
    {
        private int[] _input;
        private List<KeyValuePair<int, int>> _localMaximumIntervals;
        public SecondImplementation(int[] input)
        {
            _input = input;
        }

        public int CalculateWater()
        {
            CalculateLocalMaxIntervals();
            if (_localMaximumIntervals.Count < 2)
                return 0;

            var result = 0;
            for (int i = 0; i < _localMaximumIntervals.Count - 1; i++)
            {
                var waterHeightInInterval = Math.Min(_localMaximumIntervals[i].Value, _localMaximumIntervals[i + 1].Value);
                for (int j = _localMaximumIntervals[i].Key + 1; j < _localMaximumIntervals[i + 1].Key; j++)
                {
                    result += waterHeightInInterval - _input[j];
                }
            }

            return result;
        }

        private void CalculateLocalMaxIntervals()
        {
            _localMaximumIntervals = new List<KeyValuePair<int, int>>();
            for(int i = 0; i < _input.Length; i++)
            {
                var currentValue = _input[i];
                if (_localMaximumIntervals.Count() == 0)
                {
                    if (i < _input.Length - 1 && currentValue > _input[i + 1])
                        _localMaximumIntervals.Add(new KeyValuePair<int, int>(i, currentValue));
                }
                else if (currentValue >= _localMaximumIntervals.Last().Value)
                {
                    _localMaximumIntervals.Add(new KeyValuePair<int, int>(i, currentValue));
                }
            }
            var maxIndex = _localMaximumIntervals.Last().Key;
            var localMaximumIntervalsFromRight = new List<KeyValuePair<int, int>>();
            for (int i = _input.Length - 1; i > maxIndex; i--)
            {
                var currentValue = _input[i];
                if (localMaximumIntervalsFromRight.Count() == 0)
                {
                    if (i > maxIndex + 1 && currentValue > _input[i - 1])
                        localMaximumIntervalsFromRight.Insert(0, new KeyValuePair<int, int>(i, currentValue));
                }
                else if (currentValue >= localMaximumIntervalsFromRight.First().Value)
                {
                    localMaximumIntervalsFromRight.Insert(0, new KeyValuePair<int, int>(i, currentValue));
                }
            }
            _localMaximumIntervals = _localMaximumIntervals.Concat(localMaximumIntervalsFromRight).ToList();
        }
    }
}
