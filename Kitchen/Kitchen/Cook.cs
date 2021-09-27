using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Kitchen
{
    class Cook
    {
        Stopwatch _watch;
        private Kitchen _kitchen;
        private bool _isWorking = false;
        private string _name;
        public Cook(Stopwatch watch, Kitchen kitchen, string name)
        {
            _watch = watch;
            _kitchen = kitchen;
            _name = name;
        }
        public async Task StartWork()
        {
            _isWorking = true;
            await Task.Factory.StartNew(() => this.Work());
        }
        public void StopWork()
        {
            _isWorking = false;
        }
        public void Work()
        {
            while(_isWorking)
            {
                if (!TryProcessOrders())
                {
                    if(!TryCooking())
                        GetIngredientToPrepare();
                }
            }
        }

        private bool TryCooking()
        {
            Monitor.Enter(_kitchen._cookingLock);
            var ingredient = _kitchen.PeekNextIngredientToCook();
            var retry = false;
            if (ingredient != null)
            {
                retry = true;
                if (!_kitchen.Owen.IsInUse)
                {
                    var owen = _kitchen.Owen;
                    Monitor.Enter(owen._lock);
                    if (owen.PutIngredientIntoOwen(ingredient))
                    {
                        _kitchen.GetIngredientToCook();
                    }
                    else
                    {
                        StartOwen();
                        retry = false;
                    }
                    if (_kitchen.Owen.CurrentCount == Owen.Capacity)
                    {
                        StartOwen();
                        retry = false;
                    }
                    Monitor.Exit(owen._lock);
                }
                else
                {
                    retry = false;
                }
            }
            else
            {
                if(_kitchen.Owen.CurrentCount > 0 && !_kitchen.Owen.IsInUse && !_kitchen.HasIngredientToPrepare())
                {
                    StartOwen();
                }
            }
            Monitor.Exit(_kitchen._cookingLock);
            return retry;
        }

        private void StartOwen()
        {
            var owen = _kitchen.Owen;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[{_watch.Elapsed}] {_name}: cooking started with " + owen.CurrentCount + "x " + owen.CurrentIngredientType);
            var cooking = owen.CookIngredients();
            cooking.ContinueWith((o) =>
            {
                var currentOwen = o.Result;
                var ingredients = currentOwen.EmptyOwen();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"[{_watch.Elapsed}] " + ingredients.Count() + "x " + ingredients.First().ToString() + " cooked");
                
                foreach (var ingr in ingredients)
                {
                    ingr.IsCooked = true;
                }
            });
        }

        private bool TryProcessOrders()
        {
            var orders = _kitchen.GetOrdersToProcess();
            if (orders.Count == 0)
                return false;
            var ingredients = orders.SelectMany(s=>s.Orders.SelectMany(ss => ss.Ingredients)).OrderByDescending(o => o.NeedsCooking).ThenBy(o => o.ToString()).ToList();
            _kitchen.EnqueueIngredients(ingredients);
            foreach(var order in orders)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{_watch.Elapsed}] {_name}: {order.Name} processed");
            }
            return true;
        }

        private void GetIngredientToPrepare()
        {
            var ingredient = _kitchen.GetIngredientToPrepare();
            if (ingredient != null)
            {
                PrepareIngredient(ingredient);
            }
        }

        private void PrepareIngredient(IIngredient ingredient)
        {
            Thread.Sleep(ingredient.PreparationTime);
            ingredient.IsPrepared = true;
            if (ingredient.NeedsCooking)
            {
                _kitchen.QueueIngredientToCook(ingredient);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"[{_watch.Elapsed}] {_name}: {ingredient.ToString()} prepared for cooking");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"[{_watch.Elapsed}] {_name}: {ingredient.ToString()} prepared");
            }
        }
    }
}
