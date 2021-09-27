using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen
{
    public class Owen
    {
        public const int Capacity = 4;
        private List<IIngredient> _ingredients;
        string _currentIngredientType;
        private bool _isInUse;
        public bool IsInUse => _isInUse;
        public string CurrentIngredientType => _currentIngredientType;
        public int CurrentCount => _ingredients.Count;
        public object _lock;
        public Owen()
        {
            SetDefaultValues();
            _lock = new object();
        }

        public IEnumerable<IIngredient> EmptyOwen()
        {
            var temp = _ingredients;
            SetDefaultValues();
            return temp;
        }

        public void SetDefaultValues()
        {
            _ingredients = new List<IIngredient>();
            _currentIngredientType = null;
            _isInUse = false;
        }

        public bool PutIngredientIntoOwen(IIngredient ingredient)
        {
            if (_ingredients.Count == 0)
            {
                _ingredients.Add(ingredient);
                _currentIngredientType = ingredient.ToString();
                return true;
            }
            else
            {
                if (ingredient.ToString() != _currentIngredientType || _ingredients.Count > Capacity)
                    return false;
                _ingredients.Add(ingredient);
                return true;
            }
        }

        public async Task<Owen> CookIngredients()
        {
            _isInUse = true;
            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(_ingredients[0].CookingTime);
            });
            return this;
        }
    }
}
