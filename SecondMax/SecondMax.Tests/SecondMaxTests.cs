using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecondMax.Tests
{
    [TestClass]
    public class SecondMaxTests
    {
        [TestMethod]
        public void IncreasingValues()
        {
            var a = new[] {1, 2, 3, 4, 5};
            Assert.AreEqual(4, a.SecondMax(), "Second max value should be 4");
        }

        [TestMethod]
        public void DecreasingValues()
        {
            var a = new[] { 5, 4, 3, 2, 1 };
            Assert.AreEqual(4, a.SecondMax(), "Second max value should be 4");
        }

        [TestMethod]
        public void TenTenFive()
        {
            var a = new[] { 10, 10, 5 };
            Assert.AreEqual(5, a.SecondMax(), "Second max value should be 5");
        }

        [TestMethod]
        public void RandomValues()
        {
            var a = new[] { 4, 3, 8, 5, 2, 1, 9 };
            Assert.AreEqual(8, a.SecondMax(), "Second max value should be 8");
        }

        [TestMethod]
        public void MinusValues()
        {
            var a = new[] { -10, -100, int.MinValue };
            Assert.AreEqual(-100, a.SecondMax(), "Second max value should be -100");
        }

        [TestMethod]
        public void EmptySequence()
        {
            var a = new List<int>();
            try
            {
                var m = a.SecondMax();
                Assert.Fail("Exception expected");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual("Sequence contains no element", e.Message);
            }
        }

        [TestMethod]
        public void OneElementSequence()
        {
            var a = new List<int> { 1 };
            try
            {
                var m = a.SecondMax();
                Assert.Fail("Exception expected");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual("Sequence contains only one element or all elements are equal in sequence", e.Message);
            }
        }

        [TestMethod]
        public void AllElementsAreEqualSequence()
        {
            var a = new [] { 0, 0, 0, 0, 0, 0};
            try
            {
                var m = a.SecondMax();
                Assert.Fail("Exception expected");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual("Sequence contains only one element or all elements are equal in sequence", e.Message);
            }
        }

    }
}
