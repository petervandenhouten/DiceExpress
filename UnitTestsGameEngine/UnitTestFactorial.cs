using System;
using DiceExpressGameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestFractional
    {
        [TestMethod]
        public void Test_Factorial()
        {
            Assert.AreEqual(1, ChanceCalculator.Factorial(0));
            Assert.AreEqual(1, ChanceCalculator.Factorial(1));
            Assert.AreEqual(2, ChanceCalculator.Factorial(2));
            Assert.AreEqual(6, ChanceCalculator.Factorial(3));
            Assert.AreEqual(24, ChanceCalculator.Factorial(4));
        }
    }
}