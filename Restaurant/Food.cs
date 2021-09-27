using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public abstract class Food : IFood
    {
        public abstract double CalculateHappiness(double happiness);
    }

    public class HotDog : Food
    {
        public override double CalculateHappiness(double happiness)
        {
            return happiness + 2;
        }

        public override string ToString()
        {
            return "food=HotDog";
        }
    }

    public class Chips : Food
    {
        public override double CalculateHappiness(double happiness)
        {
            return happiness * 1.05;
        }
        public override string ToString()
        {
            return "food=Chips";
        }
    }
}
