using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeral
{
    public static class RomanNumeralConverter
    {
        private static string[] ones = {"I","X","C","M"};
        private static string[] fives = { "V", "L", "D" };

        public static string ToRoman(this int number)
        {
            if(number < 1 || number > 3999)
            {
                throw new ArgumentOutOfRangeException();
            }
            string ret = String.Empty;
            int power = 0;
            while(number > 0)
            {
                int digit = number % 10;
                if (digit != 0)
                {
                    if (digit == 9)
                    {
                        ret = ones[power] + ones[power + 1] + ret;
                    }
                    else if (digit == 4)
                    {
                        ret = ones[power] + fives[power] + ret;
                    }
                    else
                    {
                        string val = "";
                        if(digit >= 5)
                        {
                            val = fives[power];
                            digit -= 5;
                        }
                        for (int i = 0; i < digit; i++)
                        {
                            val += ones[power];
                        }
                        ret = val + ret;
                    }
                }
                number = number / 10;
                power++;
            }
            return ret;
        }
    }
}
