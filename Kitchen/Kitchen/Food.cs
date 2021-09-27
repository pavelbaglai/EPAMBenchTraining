using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace Kitchen
{
    public interface IFood
    {
        List<IIngredient> Ingredients { get; }
        bool IsReady { get; set; }
        void CheckFoodReady();
    }

    public abstract class FoodBase
    {
        private Stopwatch _watch;
        private bool _isReady;
        protected List<IIngredient> _ingredients;
        protected Order _order;
        
        public List<IIngredient> Ingredients => _ingredients;
        public bool IsReady {
            get { return _isReady; }
            set
            {
                _isReady = value;
                if (value)
                    _order.CheckOrderReady();
            }
        }
        
        public FoodBase(Order order, Stopwatch watch)
        {
            IsReady = false;
            _order = order;
            _watch = watch;
        }
        public void CheckFoodReady()
        {
            if (_ingredients.All(a => a.IsPrepared && a.IsCooked))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{_watch.Elapsed}] {this.ToString()} served");
                IsReady = true;
            }
        }
    }

    class NakedBurger : FoodBase, IFood
    {
        public NakedBurger(Order order, Stopwatch watch) : base(order, watch)
        {
            _ingredients = new List<IIngredient> { new Patty(this), new Lettuce(this), new Tomato(this), new Ketchup(this) };
        }   
    }

    class BasicBurger : FoodBase, IFood
    {
        public BasicBurger(Order order, Stopwatch watch) : base(order, watch)
        {
            _ingredients = new List<IIngredient> { new Bun(this), new Patty(this), new Ketchup(this) };
        }
    }
    class CheeseBurger : FoodBase, IFood
    {
        public CheeseBurger(Order order, Stopwatch watch) : base(order, watch)
        {
            _ingredients = new List<IIngredient> { new Bun(this), new Patty(this), new Cheese(this), new Ketchup(this) };
        }
    }
    class FullBurger : FoodBase, IFood
    {
        public FullBurger(Order order, Stopwatch watch) : base(order, watch)
        {
            _ingredients = new List<IIngredient> { new Bun(this), new Patty(this), new Lettuce(this), new Tomato(this), new Cheese(this), new Ketchup(this) };
        }
    }
    class DoubleBurger : FoodBase, IFood
    {
        public DoubleBurger(Order order, Stopwatch watch) : base(order, watch)
        {
            _ingredients = new List<IIngredient> { new Bun(this), new Patty(this), new Patty(this), new Lettuce(this), new Tomato(this), new Cheese(this), new Cheese(this), new Ketchup(this) };
        }
    }
    class FrenchFries : FoodBase, IFood
    {
        public FrenchFries(Order order, Stopwatch watch) : base(order, watch)
        {
            _ingredients = new List<IIngredient> { new Fries(this) };
        }
    }
    class FrenchFriesWithKetchup : FoodBase, IFood
    {
        public FrenchFriesWithKetchup(Order order, Stopwatch watch) : base(order, watch)
        {
            _ingredients = new List<IIngredient> { new Fries(this), new Ketchup(this) };
        }
    }
}
