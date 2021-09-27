using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public abstract class Extra : IFood
    {
        public abstract double CalculateHappiness(double happiness);
        protected IFood Food;
        internal Extra(IFood food)
        {
            Food = food;
        }
    }

    public class Ketchup : Extra
    {
        public override double CalculateHappiness(double happiness)
        {
            return Food.CalculateHappiness(Food.CalculateHappiness(happiness));
        }

        public Ketchup(IFood food) : base(food)
        {
            
        }
        public override string ToString()
        {
            return $"Ketchup[{Food}]";
        }
    }

    public class Mustard : Extra
    {
        public override double CalculateHappiness(double happiness)
        {
            return happiness + 1;
        }

        public Mustard(IFood food) : base(food)
        {

        }
        public override string ToString()
        {
            return $"Mustard[{Food}]";
        }
    }
}
