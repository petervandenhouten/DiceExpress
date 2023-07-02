using System;
using DiceExpressGameEngine;
using Mehroz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestBinomialDistribution
    {
        [TestMethod]
        public void Probability_That_Two_Dices_Have_The_Same_Result()
        {
            int n = 2; // 2 trails
            int x = 2; // event must exactly occurs twice
            var p = new Fraction(1, 6); // probability of the event

            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.BinomialDistribution(n, x, p));

        }
    }
}