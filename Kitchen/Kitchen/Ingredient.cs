using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    public interface IIngredient
    {
        bool NeedsCooking { get; }
        int PreparationTime { get; }
        int CookingTime { get; }
        bool IsPrepared { get; set; }
        bool IsCooked { get; set; }
        IFood Food { get; }
    }

    public abstract class IngredientBase
    {
        private bool _isPrepared;
        private bool _isCooked;
        public bool IsPrepared
        {
            get { return _isPrepared; }
            set
            {
                _isPrepared = value;
                if(value && IsPrepared && IsCooked)
                {
                    Food.CheckFoodReady();
                }
            }
        }
        public bool IsCooked
        {
            get { return _isCooked; }
            set
            {
                _isCooked = value;
                if (value && IsPrepared && IsCooked)
                {
                    Food.CheckFoodReady();
                }
            }
        }
        public IFood Food { get; }
        public IngredientBase(IFood food)
        {
            IsPrepared = false;
            IsCooked = false;
            Food = food;
        }
    }

    class Bun : IngredientBase, IIngredient
    {
        public bool NeedsCooking => false;

        public int PreparationTime => 200;

        public int CookingTime => 0;
        
        public Bun(IFood food) : base(food)
        {
            IsCooked = true;
        }
    }

    class Patty : IngredientBase, IIngredient
    {
        public bool NeedsCooking => true;

        public int PreparationTime => 100;

        public int CookingTime => 1000;

        public Patty(IFood food) : base(food)
        {

        }
    }
    class Lettuce : IngredientBase, IIngredient
    {
        public bool NeedsCooking => false;

        public int PreparationTime => 200;

        public int CookingTime => 0;

        public Lettuce(IFood food) : base(food)
        {
            IsCooked = true;
        }
    }
    class Tomato : IngredientBase, IIngredient
    {
        public bool NeedsCooking => false;

        public int PreparationTime => 200;

        public int CookingTime => 0;

        public Tomato(IFood food) : base(food)
        {
            IsCooked = true;
        }
    }
    class Cheese : IngredientBase, IIngredient
    {
        public bool NeedsCooking => false;

        public int PreparationTime => 0;

        public int CookingTime => 0;

        public Cheese(IFood food) : base(food)
        {
            IsCooked = true;
        }
    }
    class Ketchup : IngredientBase, IIngredient
    {
        public bool NeedsCooking => false;

        public int PreparationTime => 0;

        public int CookingTime => 0;

        public Ketchup(IFood food) : base(food)
        {
            IsCooked = true;
        }
    }
    class Fries : IngredientBase, IIngredient
    {
        public bool NeedsCooking => true;

        public int PreparationTime => 100;

        public int CookingTime => 2000;

        public Fries(IFood food) : base(food)
        {
            
        }
    }
}
