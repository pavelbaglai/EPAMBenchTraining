using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Order
    {
        public IEnumerable<string> Extras { get; }
        public string Food { get; }

        public Order(string food, IEnumerable<string> extras)
        {
            Extras = extras;
            Food = food;
        }
        public void NotifyReady(IFood food)
        {
            Console.WriteLine($"Order: Notifying observers of {this}");
            FoodReady(this, new FoodReadyEventArgs() { Food = food });
            Console.WriteLine("Oder: Notification done.");
        }

        public event EventHandler<FoodReadyEventArgs> FoodReady;

        public override string ToString()
        {
            return $"Order [food={Food}, extras=[{String.Join(",",Extras)}]]";
        }
    }
}
