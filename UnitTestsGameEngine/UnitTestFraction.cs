using System;
using Mehroz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestFraction
    {
        [TestMethod]
        public void Power_of_fraction()
        {
            var f1 = new Fraction(2, 3);
            var f2 = f1 ^ 2;

            Assert.AreEqual(new Fraction(4, 9), f2);

            var f3 = new Fraction(1, 6);
            var f4 = f3 ^ 3;

            Assert.AreEqual(new Fraction(1, 216), f4);

        }
    }
}