using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant;

namespace RestaurantTest
{
    [TestClass]
    public class RestaurantTest
    {
        [TestMethod]
        public void HotDogHappinessTest()
        {
            IFood food = new HotDog();
            double expected = 102;
            double startingHappiness = 100;
            double happiness = food.CalculateHappiness(startingHappiness);
            Assert.AreEqual(happiness, expected);
        }

        [TestMethod]
        public void HotDogWithKetchupHappinessTest()
        {
            IFood food = new HotDog();
            food = new Ketchup(food);
            double expected = 104;
            double startingHappiness = 100;
            double happiness = food.CalculateHappiness(startingHappiness);
            Assert.AreEqual(happiness, expected);
        }
        [TestMethod]
        public void HotDogWithMustardHappinessTest()
        {
            IFood food = new HotDog();
            food = new Mustard(food);
            double expected = 101;
            double startingHappiness = 100;
            double happiness = food.CalculateHappiness(startingHappiness);
            Assert.AreEqual(happiness, expected);
        }

        [TestMethod]
        public void ChipsHappinessTest()
        {
            IFood food = new Chips();
            double expected = 105;
            double startingHappiness = 100;
            double happiness = food.CalculateHappiness(startingHappiness);
            Assert.AreEqual(happiness, expected);
        }

        [TestMethod]
        public void ChipsWithKetchupHappinessTest()
        {
            IFood food = new Chips();
            food = new Ketchup(food);
            double expected = 110.25;
            double startingHappiness = 100;
            double happiness = food.CalculateHappiness(startingHappiness);
            Assert.AreEqual(happiness, expected);
        }
        [TestMethod]
        public void ChipsWithMustardHappinessTest()
        {
            IFood food = new Chips();
            food = new Mustard(food);
            double expected = 101;
            double startingHappiness = 100;
            double happiness = food.CalculateHappiness(startingHappiness);
            Assert.AreEqual(happiness, expected);
        }
    }
}
