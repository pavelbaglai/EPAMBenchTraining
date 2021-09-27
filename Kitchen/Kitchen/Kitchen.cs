using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Kitchen
{
    class Kitchen
    {
        public Owen Owen;
        private Queue<IIngredient> _ingredientsToPrepare;
        private Queue<IIngredient> _ingredientToCook;
        private Queue<Order> _ordersToProcess;
        private Stopwatch _watch;
        private object _ingredientLock;
        public object _cookingLock;
        private object _orderLock;

        public Kitchen(Stopwatch watch)
        {
            _watch = watch;
            _ingredientLock = new object();
            _cookingLock = new object();
            _orderLock = new object();
            _ingredientsToPrepare = new Queue<IIngredient>();
            _ingredientToCook = new Queue<IIngredient>();
            _ordersToProcess = new Queue<Order>();
            Owen = new Owen();
        }
        public void RecieveOrders(List<Order> orders)
        {
            Monitor.Enter(_orderLock);
            foreach(var order in orders)
            {
                _ordersToProcess.Enqueue(order);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{_watch.Elapsed}] Order taken: {order.Name}");
            }
            Monitor.Exit(_orderLock);
        }

        public IIngredient GetIngredientToPrepare()
        {
            IIngredient ingredient;
            Monitor.Enter(_ingredientLock);
            if (_ingredientsToPrepare.Count == 0)
                ingredient = null;
            else
                ingredient = _ingredientsToPrepare.Dequeue();
            Monitor.Exit(_ingredientLock);
            return ingredient;
        }

        public void QueueIngredientToCook(IIngredient ingredient)
        {
            Monitor.Enter(_ingredientLock);
            _ingredientToCook.Enqueue(ingredient);
            Monitor.Exit(_ingredientLock);
        }

        public List<Order> GetOrdersToProcess()
        {
            List<Order> orders = new List<Order>();
            Monitor.Enter(_orderLock);
            while (_ordersToProcess.Count != 0)
            {
                orders.Add(_ordersToProcess.Dequeue());
            }
            Monitor.Exit(_orderLock);
            return orders;
        }

        public void EnqueueIngredients(IEnumerable<IIngredient> ingredients)
        {
            Monitor.Enter(_ingredientLock);
            foreach(var ingredient in ingredients)
            {
                _ingredientsToPrepare.Enqueue(ingredient);
            }
            Monitor.Exit(_ingredientLock);
        }

        public IIngredient PeekNextIngredientToCook()
        {
            IIngredient ingredient;
            _ingredientToCook.TryPeek(out ingredient);
            return ingredient;
        }

        public IIngredient GetIngredientToCook()
        {
            return _ingredientToCook.Dequeue();
        }

        public bool HasIngredientToPrepare()
        {
            return _ingredientsToPrepare.Count > 0;
        }
    }
}
