using System;
using System.Collections.Generic;
using System.Linq;

namespace SecondMax
{
    public static class EnumarableExtensions
    {
        public static T SecondMax<T>(this IEnumerable<T> list) where T:IComparable
        {
            if (list.Count() == 0)
                throw new ArgumentException("Sequence contains no element");
            if (list.Count() == 1)
                throw new ArgumentException("Sequence contains only one element or all elements are equal in sequence");

            T max = list.First();
            T secondMax = max;
            int i = 1;
            bool hasSecondMax = false;
            while(i < list.Count())
            {
                T current = list.ElementAt(i);
                int comparison = max.CompareTo(current);
                if(comparison < 0)
                {
                    secondMax = max;
                    max = current;
                    hasSecondMax = true;
                }
                else if(comparison > 0)
                {
                    if (hasSecondMax)
                    {
                        if(secondMax.CompareTo(current) < 0)
                        {
                            secondMax = current;
                        }
                    }
                    else
                    {
                        secondMax = current;
                        hasSecondMax = true;
                    }
                }
                i++;
            }
            if(!hasSecondMax)
                throw new ArgumentException("Sequence contains only one element or all elements are equal in sequence");
            return secondMax;
        }
    }
}