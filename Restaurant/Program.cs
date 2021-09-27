using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            var kitchen = new Kitchen();
            var waitress = new Waitress(kitchen);
            var client1 = new Client("Peter", 100);
            var client2 = new Client("Berci", 200);
            var order1 = new Order("HotDog", new List<string>(){ "Ketchup" });
            var order2 = new Order("Chips", new List<string>() { "Mustard" });
            waitress.TakeOrder(client1, order1);
            waitress.TakeOrder(client2, order2);
            waitress.ServerOrders();
            Console.ReadLine();
        }
    }
}
