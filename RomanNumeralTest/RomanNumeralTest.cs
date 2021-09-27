using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomanNumeral;

namespace RomanNumeralTest
{
    [TestClass]
    public class RomanNumeralTest
    {
        [DataRow(1, "I")]
        [DataRow(2, "II")]
        [DataRow(4, "IV")]
        [DataRow(5, "V")]
        [DataRow(9, "IX")]
        [DataRow(12, "XII")]
        [DataRow(14, "XIV")]
        [DataRow(19, "XIX")]
        [DataRow(40, "XL")]
        [DataRow(99, "XCIX")]
        [DataRow(318, "CCCXVIII")]
        [DataRow(400, "CD")]
        [DataRow(555, "DLV")]
        [DataRow(999, "CMXCIX")]
        [DataRow(2500, "MMD")]
        [DataRow(3999, "MMMCMXCIX")]
        [DataTestMethod]
        public void ToRomanTestValidNumbers(int number, string expected)
        {
            string result;
            result = number.ToRoman();
            Assert.AreEqual(result, expected);

        }
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(4000)]
        [DataTestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToRomanTestInvalidNumbers(int number)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => number.ToRoman());
        }
    }
}

