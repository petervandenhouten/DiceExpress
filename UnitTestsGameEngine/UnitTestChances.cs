using DiceExpressGameEngine;
using Mehroz;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestChances
    {
        [TestMethod]
        public void Given_One_Dice_When_Line_Contains_One_Symbol()
        {
            Assert.AreEqual(new Fraction(1, 6), ChanceCalculator.GetProbabilityForLine("A",1));
            Assert.AreEqual(new Fraction(1, 6), ChanceCalculator.GetProbabilityForLine("B",1));
            Assert.AreEqual(new Fraction(1, 6), ChanceCalculator.GetProbabilityForLine("C",1));
        }

        [TestMethod]
        public void Given_Four_Dices_When_Line_Contains_One_Symbol()
        {
            Assert.AreEqual(new Fraction(671, 1296), ChanceCalculator.GetProbabilityForLine("A", 4));
            Assert.AreEqual(new Fraction(671, 1296), ChanceCalculator.GetProbabilityForLine("B", 4));
            Assert.AreEqual(new Fraction(671, 1296), ChanceCalculator.GetProbabilityForLine("C", 4));
        }

        [TestMethod]
        public void When_Line_Contains_Unknown_Symbol()
        {
            Assert.AreEqual(new Fraction(0), ChanceCalculator.GetProbabilityForLine("X", 1));
            Assert.AreEqual(new Fraction(0), ChanceCalculator.GetProbabilityForLine("YY", 3));
        }

        [TestMethod]
        public void When_No_Dice()
        {
            Assert.AreEqual(new Fraction(0), ChanceCalculator.GetProbabilityForLine("A", 0));
        }

        [TestMethod]
        public void Given_Two_Dices_When_Line_Contains_Two_Identical_Symbols()
        {
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("AA",2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("BB",2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("CC",2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("DD", 2));
        }

        [TestMethod]
        public void Given_Three_Dices_When_Line_Contains_Two_Identical_Symbols()
        {
            Assert.AreEqual(new Fraction(2, 27), ChanceCalculator.GetProbabilityForLine("AA", 3));
            Assert.AreEqual(new Fraction(2, 27), ChanceCalculator.GetProbabilityForLine("BB", 3));
            Assert.AreEqual(new Fraction(2, 27), ChanceCalculator.GetProbabilityForLine("CC", 3));
            Assert.AreEqual(new Fraction(2, 27), ChanceCalculator.GetProbabilityForLine("DD", 3));
        }
        [TestMethod]
        public void Given_More_Dices_Then_Propability_Increased()
        {
            var d1 = ChanceCalculator.GetProbabilityForLine("AA", 1);
            var d2 = ChanceCalculator.GetProbabilityForLine("AA", 2);
            var d3 = ChanceCalculator.GetProbabilityForLine("AA", 3);
            var d4 = ChanceCalculator.GetProbabilityForLine("AA", 4);
            var d5 = ChanceCalculator.GetProbabilityForLine("AA", 5);
            var d6 = ChanceCalculator.GetProbabilityForLine("AA", 6);
            var d7 = ChanceCalculator.GetProbabilityForLine("AA", 7);

            Assert.AreEqual(new Fraction(0), d1);
            Assert.AreEqual(new Fraction(1,36), d2);
            Assert.AreEqual(new Fraction(2,27), d3);
            Assert.IsTrue(d2 > d1);
            Assert.IsTrue(d3 > d2);
            Assert.IsTrue(d4 > d3);
            Assert.IsTrue(d5 > d4);
            Assert.IsTrue(d6 > d5);
            Assert.IsTrue(d7 > d6);
        }

        [TestMethod]
        public void Given_Two_Dices_When_Line_Contains_Two_Different_Symbols()
        {
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("AB", 2));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("AC", 2));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("BA", 2));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("BC", 2));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("CA", 2));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("CB", 2));
        }

        [TestMethod]
        public void When_Combination_Of_Different_Two_Different_Symbols_Then_Order_Does_Not_Matter()
        {
            var p1 = ChanceCalculator.GetProbabilityForLine("AB", 5);
            var p2 = ChanceCalculator.GetProbabilityForLine("BA", 5);

            Assert.AreEqual(p1, p2);

        }
        [TestMethod]
        public void Given_Three_Dices_When_Line_Contains_Two_Different_Symbols()
        {
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("AB", 3));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("AC", 3));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("BA", 3));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("BC", 3));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("CA", 3));
            Assert.AreEqual(new Fraction(2, 36), ChanceCalculator.GetProbabilityForLine("CB", 3));
        }

        [TestMethod]
        public void Given_Two_Dices_When_Line_Contains_Numbers()
        {
            Assert.AreEqual(new Fraction(0), ChanceCalculator.GetProbabilityForLine("10", 2));
            Assert.AreEqual(new Fraction(0), ChanceCalculator.GetProbabilityForLine("9", 2));
            Assert.AreEqual(new Fraction(0), ChanceCalculator.GetProbabilityForLine("8", 2));
            Assert.AreEqual(new Fraction(0), ChanceCalculator.GetProbabilityForLine("7", 2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("6", 2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("5", 2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("4", 2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("3", 2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("2", 2));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("1", 2));
        }

        [TestMethod]
        public void Given_Four_Dices_When_Line_Contains_Numbers()
        {
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("10",4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("9", 4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("8", 4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("7", 4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("6", 4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("5", 4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("4", 4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("3", 4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("2", 4));
            Assert.AreEqual(new Fraction(1, 36), ChanceCalculator.GetProbabilityForLine("1", 4));
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