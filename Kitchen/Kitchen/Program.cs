using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kitchen
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            Kitchen kitchen = new Kitchen(watch);
            Cook cook1 = new Cook(watch, kitchen, "Cook1");
            Cook cook2 = new Cook(watch, kitchen, "Cook2");
            //Cook cook3 = new Cook(watch, kitchen, "Cook3");

            Order order1;
            order1 = new Order("Order1", watch);
            order1.SetOrderItems(new List<IFood>()
            {
                new BasicBurger(order1, watch),
                new BasicBurger(order1, watch),
                new CheeseBurger(order1, watch),
                new CheeseBurger(order1, watch),
                new DoubleBurger(order1, watch),
                new DoubleBurger(order1, watch),
                new FullBurger(order1, watch),
                new FullBurger(order1, watch),
                new FrenchFriesWithKetchup(order1, watch),
                new FrenchFriesWithKetchup(order1, watch),
                new FrenchFriesWithKetchup(order1, watch),
                new FrenchFriesWithKetchup(order1, watch),
                new FrenchFries(order1, watch),
                new FrenchFries(order1, watch),
                new FrenchFries(order1, watch),
            });
            Order order2;
            order2 = new Order("Order2", watch);
            order2.SetOrderItems(new List<IFood>()
            {
                new BasicBurger(order2, watch),
                new BasicBurger(order2, watch),
                new CheeseBurger(order2, watch),
                new CheeseBurger(order2, watch),
                new DoubleBurger(order2, watch),
                new DoubleBurger(order2, watch),
                new FullBurger(order2, watch),
                new FullBurger(order2, watch),
                new FrenchFriesWithKetchup(order2, watch),
                new FrenchFriesWithKetchup(order2, watch),
                new FrenchFriesWithKetchup(order2, watch),
                new FrenchFriesWithKetchup(order2, watch),
                new FrenchFries(order2, watch),
                new FrenchFries(order2, watch),
                new FrenchFries(order2, watch),
            });
            var orders = new List<Order>() { order1 };
            watch.Start();
            kitchen.RecieveOrders(orders);
            var t = cook1.StartWork();
            var t2 = cook2.StartWork();
            t.Wait();
            //t2.Wait();
        }
    }
}
