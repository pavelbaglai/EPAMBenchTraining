using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationalNumbers
{
    public struct RationalNumber : IEquatable<RationalNumber>, IComparable<RationalNumber>
    {
        private int numerator;
        private int denominator;
        public RationalNumber(int num, int denom)
        {
            if (denom == 0)
                throw new DivideByZeroException();
            int gcd = Math.Abs(Gcd(num, denom));
            
            if (num < 0 || denom < 0)
            {
                num = Math.Abs(num) * (num < 0 && denom < 0 ? 1 : -1);
                denom = Math.Abs(denom);
            }
            numerator = num / gcd;
            denominator = denom / gcd;
        }

        public bool Equals(RationalNumber number)
        {
            return numerator == number.numerator && denominator == number.denominator;
        }

        public int CompareTo(RationalNumber number)
        {
            if (this.Equals(number))
                return 0;
            int scm = Scm(this.denominator, number.denominator);
            return numerator * scm / this.denominator > number.numerator * scm / number.denominator ? 1 : -1;
        }

        public override string ToString()
        {
            return numerator.ToString() + (denominator == 1 ? "" : "r" + denominator);
        }

        public static RationalNumber operator+(RationalNumber x, RationalNumber y)
        {
            int scm = Scm(x.denominator, y.denominator);
            return new RationalNumber(x.numerator * scm / x.denominator + y.numerator * scm / y.denominator, scm);
        }

        public static RationalNumber operator -(RationalNumber x, RationalNumber y)
        {
            int scm = Scm(x.denominator, y.denominator);
            return new RationalNumber(x.numerator * scm / x.denominator - y.numerator * scm / y.denominator, scm);
        }

        public static RationalNumber operator *(RationalNumber x, RationalNumber y)
        {
            return new RationalNumber(x.numerator * y.numerator, x.denominator * y.denominator);
        }

        public static RationalNumber operator /(RationalNumber x, RationalNumber y)
        {
            return new RationalNumber(x.numerator * y.denominator, x.denominator * y.numerator);
        }
        public static RationalNumber operator -(RationalNumber x)
        {
            return new RationalNumber(x.numerator * -1, x.denominator);
        }
        public static implicit operator RationalNumber(int num)
        {
            return new RationalNumber(num, 1);
        }
        private static int Gcd(int x, int y)
        {
            return y == 0 ? x : Gcd(y, x % y);
        }

        private static int Scm(int x, int y)
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
    }
}
