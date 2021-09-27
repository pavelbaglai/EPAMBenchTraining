using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationalNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            /*RationalNumber num1 = new RationalNumber(3, 4);
            RationalNumber num2 = new RationalNumber(12, 16);
            RationalNumber num3 = new RationalNumber(-2, 15);
            RationalNumber num4 = new RationalNumber(-10, -20);
            RationalNumber num5 = new RationalNumber(3, -7);
            Console.WriteLine(num1);
            Console.WriteLine(num2);
            Console.WriteLine(num3);
            Console.WriteLine(num4);
            Console.WriteLine(num5);
            Console.WriteLine(num1*num3);
            Console.WriteLine(num1 + num5);
            Console.WriteLine(num3 - num4);
            Console.WriteLine(num2 / num3);
            Console.WriteLine(-num2);*/
            Console.WriteLine((RationalNumber)3);
            Console.ReadLine();
        }
        /*
         10409754-00221091- 
         * */
        /*public static int Scm(int x, int y)
        {
            if (x == y)
                return x;
            int max = Math.Max(x, y);
            int min = Math.Min(x, y);
            int num = min;
            while (num % max != 0)
            {
                num += min;
            }
            return num;
        }

        private static int Gcd(int x, int y)
        {
            return y == 0 ? x : Gcd(y, x % y);
        }*/
    }
}
