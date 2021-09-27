using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Waitress
    {
        private Queue<Order> Orders;
        private Kitchen Kitchen;

        public Waitress(Kitchen kitchen)
        {
            Kitchen = kitchen;
            Orders = new Queue<Order>();
        }

        public void TakeOrder(Client client, Order order)
        {
            Console.WriteLine($"WaitressRobot: Order registered, client:{client}, order: {order}");
            /*Action<object, FoodReadyEventArgs> func = (sender, args) => { client.Eat(args.Food); };
            order.FoodReady += new EventHandler<FoodReadyEventArgs>(func);*/
            order.FoodReady += (sender, args) => { client.Eat(args.Food); };
            Orders.Enqueue(order);
        }

        public void ServerOrders()
        {
            Console.WriteLine($"WaitressRobot: Processing {Orders.Count} order(s)...");
            foreach(var order in Orders)
            {
                IFood food = Kitchen.Cook(order);
                order.NotifyReady(food);
            }
            Console.WriteLine("WaitressRobot: Orders processed.");
        }
    }
}
