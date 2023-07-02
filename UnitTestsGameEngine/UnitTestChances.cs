using DiceExpressGameEngine;
using Mehroz;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestChances
    {
        [TestMethod]
        public void When_Line_Contains_One_Symbol()
        {
            Assert.AreEqual(new Fraction(1, 6), ChanceCalculator.GetProbabilityForLine("A"));
            Assert.AreEqual(new Fraction(1, 6), ChanceCalculator.GetProbabilityForLine("B"));
            Assert.AreEqual(new Fraction(1, 6), ChanceCalculator.GetProbabilityForLine("C"));
        }

        [TestMethod]
        public void When_Line_Contains_Unknown_Symbol()
        {
            Assert.AreEqual(new Fraction(0), ChanceCalculator.GetProbabilityForLine("X"));
        }

        [TestMethod]
        public void When_Line_Contains_Two_Identical_Symbols()
        {
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("AA"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("BB"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("CC"));
        }

        [TestMethod]
        public void When_Line_Contains_Two_Different_Symbols()
        {
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("AB"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("AC"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("BA"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("BC"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("CA"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("CB"));
        }

        [TestMethod]
        public void When_Line_Contains_Number()
        {
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("10"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("9"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("8"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("7"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("6"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("5"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("4"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("3"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("2"));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("1"));
        }

        [TestMethod]
        public void Probality_of_throwing_at_least_one_time_a_symbol_in_one_throw_of_multiple_dices()
        {
            var f1 = ChanceCalculator.GetProbabilityForSymbolWithMultipleDices(1);
            var f3 = ChanceCalculator.GetProbabilityForSymbolWithMultipleDices(3);
            var f6 = ChanceCalculator.GetProbabilityForSymbolWithMultipleDices(6);

            Assert.AreEqual(new Fraction(1, 6), f1);
            Assert.IsTrue(f3 > f1);
            Assert.IsTrue(f6 > f3);
        }

        [TestMethod]
        public void Probality_of_throwing_at_least_one_time_a_symbol_in_multiple_throws()
        {
            var f7 = ChanceCalculator.GetProbabilityForSymbolInAllowedThrows(7);
            var f6 = ChanceCalculator.GetProbabilityForSymbolInAllowedThrows(6);
            var f1 = ChanceCalculator.GetProbabilityForSymbolInAllowedThrows(1);

            Assert.AreEqual(new Fraction(1, 6), f1);
            Assert.IsTrue(f6 > f1);
            Assert.IsTrue(f7 > f6);

        }

        [TestMethod]
        public void Probality_of_throwing_at_least_one_double_symbol_with_multiple_dices()
        {
            var f0 = ChanceCalculator.GetProbabilityForCombinationWithMultipleDices(0);
            var f1 = ChanceCalculator.GetProbabilityForCombinationWithMultipleDices(1);
            var f2 = ChanceCalculator.GetProbabilityForCombinationWithMultipleDices(2);
            var f3 = ChanceCalculator.GetProbabilityForCombinationWithMultipleDices(3);
            var f7 = ChanceCalculator.GetProbabilityForCombinationWithMultipleDices(7);

            Assert.AreEqual(0, f0);
            Assert.AreEqual(0, f1);
            Assert.AreEqual(new Fraction(1, 36), f2);
            Assert.IsTrue(f3 > f2);
            Assert.IsTrue(f7 > f3);

        }
    }
}