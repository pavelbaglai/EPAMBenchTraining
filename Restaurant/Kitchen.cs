using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Kitchen
    {
        private IFood CreateMainFood(string food)
        {
            if (food == nameof(HotDog))
                return new HotDog();
            return new Chips();
        }

        private IFood AddExtras(IFood mainFood, IEnumerable<string> extras)
        {
            IFood food = mainFood;
            foreach(var extra in extras)
            {
                if (extra == nameof(Ketchup))
                    food = new Ketchup(food);
                else
                    food = new Mustard(food);
            }
            return food;
        }

        internal IFood Cook(Order order)
        {
            Console.WriteLine($"Kitchen: Prepairing food, order: {order}");
            IFood food = CreateMainFood(order.Food);
            food = AddExtras(food, order.Extras);
            Console.WriteLine($"Kitchen: Food prepared, food: {food}");
            return food;
        }
    }
}
