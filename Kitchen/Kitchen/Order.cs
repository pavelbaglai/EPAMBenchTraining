using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace Kitchen
{
    public class Order
    {
        private List<IFood> _orders;
        private Stopwatch _watch;
        public List<IFood> Orders => _orders;
        public string Name { get; }

        public Order(string name, Stopwatch watch)
        {
            Name = name;
            _watch = watch;
        }

        public void SetOrderItems(List<IFood> food)
        {
            _orders = food;
        }
        public void CheckOrderReady()
        {
            if (_orders.All(a => a.IsReady))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{_watch.Elapsed}] {Name} completed");
            }
        }
    }
}
