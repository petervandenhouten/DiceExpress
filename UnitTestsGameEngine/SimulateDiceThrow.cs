using System;
using DiceExpressGameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class SimulateDiceThrow
    {
        private double StdDev(IEnumerable<int> values)
        {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }

        [TestMethod]
        public void When_Die_Is_Throw()
        {
            const long n = 200000;

            var histogram = new Dictionary<string, int>();
            histogram.Add("A", 0);
            histogram.Add("B", 0);
            histogram.Add("C", 0);
            histogram.Add("1", 0);
            histogram.Add("2", 0);
            histogram.Add("3", 0);

            for (int i = 0; i < n; i++)
            {
                var c = new Dice();
                histogram[c.ToString()]++;
            }

            foreach (var entry in histogram)
            {
                Console.WriteLine("{0}: {1}", entry.Key, entry.Value);
            }

            var avg = histogram.Values.Average();
            var stddev = StdDev(histogram.Values.ToList());
            Console.WriteLine("Average: {0}", (int)avg);
            Console.WriteLine("StdDev: {0}", (int)stddev);

            foreach (var entry in histogram)
            {
                Assert.IsTrue(entry.Value > avg - 3 * stddev);
                Assert.IsTrue(entry.Value < avg + 3 * stddev);
            }

        }

        [TestMethod]
        public void When_3_Dice_Are_Thrown_What_Is_The_Probability_Of_Two_Symbols()
        {
            const long n = 150000;
            const int d = 3;
            long x = 0;
            for (int i = 0; i < n; i++)
            {
                var dices = DiceThrower.Throw(d);

                if (dices.HasAtLeast("A", 1) || dices.HasAtLeast("B", 1))
                {
                    x++;
                }
            }

            double p_sim = (double)x / n;

            Console.WriteLine("Probability simulated {0:0.00}%", p_sim * 100);

            var f = ChanceCalculator.GetProbabilityForSymbolWithMultipleDices(d);
            double p_formula = f.ToDouble();
            Console.WriteLine("Probability formula {0:0.00}%", p_formula * 100);

            double difference = Math.Abs(p_sim - p_formula);
            double rel_dif = difference / p_formula;
            Console.WriteLine("Relative difference {0:0.00}%", rel_dif * 100);

            Assert.IsTrue(Math.Abs(rel_dif) < 0.05);
        }

        [TestMethod]
        public void When_7_Dice_Are_Thrown_What_Is_The_Probability_Of_At_Least_One_Symbol()
        {
            const long n = 150000;
            const int d = 7;
            long x = 0;
            for (int i = 0; i < n; i++)
            {
                var dices = DiceThrower.Throw(d);

                if (dices.HasAtLeast("A", 1))
                {
                    x++;
                }
            }

            double p_sim = (double)x / n;

            Console.WriteLine("Probability simulated {0:0.00}%", p_sim * 100);

            var f = ChanceCalculator.GetProbabilityForSymbolWithMultipleDices(d);
            double p_formula = f.ToDouble();
            Console.WriteLine("Probability formula {0:0.00}%", p_formula * 100);

            double difference = Math.Abs(p_sim - p_formula);
            double rel_dif = difference / p_formula;
            Console.WriteLine("Relative difference {0:0.00}%", rel_dif * 100);

            Assert.IsTrue(Math.Abs(rel_dif) < 0.05);
        }


        [TestMethod]
        public void When_7_Dice_Are_Thrown_What_Is_The_Probability_Of_At_Least_Two_Symbols()
        {
            const long n = 150000;
            long x = 0;
            int d = 7;
            for (int i = 0; i < n; i++)
            {
                var dices = DiceThrower.Throw(d);

                if (dices.HasAtLeast("A", 2))
                {
                    x++;
                }
            }

            double p_sim = (double)x / n;

            Console.WriteLine("Probability simulated {0:0.00}%", p_sim * 100);

            //var f = ChanceCalculator.GetProbabilityForCombinationWithMultipleDices(d);
            var f = ChanceCalculator.GetProbabilityForLine("AA", 7);
            double p_formula = f.ToDouble();
            Console.WriteLine("Probability formula {0:0.00}%", p_formula * 100);

            double difference = Math.Abs(p_sim - p_formula);
            double rel_dif = difference / p_formula;
            Console.WriteLine("Relative difference {0:0.00}%", rel_dif * 100);

            Assert.IsTrue(Math.Abs(rel_dif) < 0.05);

        }

        [TestMethod]
        public void When_1_Die_Is_Thrown_What_Is_The_Probability_Of_A_Symbols()
        {
            const long n = 10000;
            long x = 0;
            int d = 1;
            for (int i = 0; i < n; i++)
            {
                var dices = DiceThrower.Throw(d);

                if (dices.HasAtLeast("3", 1))
                {
                    x++;
                }
            }

            double p_sim = (double)x / n;
            Console.WriteLine("Probability simulated {0:0.00}%", p_sim * 100);

            var f = ChanceCalculator.GetProbabilityForLine("A", 1);
            double p_formula = f.ToDouble();
            Console.WriteLine("Probability formula {0:0.00}%", p_formula * 100);

            double difference = Math.Abs(p_sim - p_formula);
            double rel_dif = difference / p_formula;
            Console.WriteLine("Relative difference {0:0.00}%", rel_dif * 100);

            var expected = (f * n).ToDouble();
            var error = Math.Abs(expected - x);
            var rel_error = error / expected;

            Assert.IsTrue(Math.Abs(rel_error) < 0.05);

        }

        [TestMethod]
        public void When_2_Dice_Are_Thrown_What_Is_The_Probability_Of_A_Combination_Of_Two_Different_Symbols()
        {
            const long n = 150000;
            long x = 0;
            int d = 2;
            for (int i = 0; i < n; i++)
            {
                var dices = DiceThrower.Throw(d);

                if (dices.HasAtLeast("A", 1) && dices.HasAtLeast("B", 1))
                {
                    x++;
                }
            }

            double p_sim = (double)x / n;

            Console.WriteLine("Probability simulated {0:0.00}%", p_sim * 100);

            var f = ChanceCalculator.GetProbabilityForLine("AB", 2);
            double p_formula = f.ToDouble();
            Console.WriteLine("Probability formula {0:0.00}%", p_formula * 100);

            double difference = Math.Abs(p_sim - p_formula);
            double rel_dif = difference / p_formula;
            Console.WriteLine("Relative difference {0:0.00}%", rel_dif * 100);

            var expected = (f * n).ToDouble();
            var error = Math.Abs(expected - x);
            var rel_error = error / expected;

            Assert.IsTrue(Math.Abs(rel_error) < 0.05);
            //Assert.IsTrue(Math.Abs(rel_dif) < 0.05);

        }

        [TestMethod]
        public void When_2_Dice_Are_Thrown_What_Is_The_Probability_Of_A_Combination_Of_Two_Identical_Symbols()
        {
            const long n = 150000;
            long x = 0;
            int d = 2;
            for (int i = 0; i < n; i++)
            {
                var dices = DiceThrower.Throw(d);

                if (dices.HasAtLeast("A", 2))
                {
                    x++;
                }
            }

            double p_sim = (double)x / n;

            Console.WriteLine("Probability simulated {0:0.00}%", p_sim * 100);

            var f = ChanceCalculator.GetProbabilityForLine("AA", 2);
            double p_formula = f.ToDouble();
            Console.WriteLine("Probability formula {0:0.00}%", p_formula * 100);

            double difference = Math.Abs(p_sim - p_formula);
            double rel_dif = difference / p_formula;
            Console.WriteLine("Relative difference {0:0.00}%", rel_dif * 100);

            var expected = (f * n).ToDouble();
            var error = Math.Abs(expected - x);
            var rel_error = error / expected;

            Assert.IsTrue(Math.Abs(rel_error) < 0.05);
            //Assert.IsTrue(Math.Abs(rel_dif) < 0.05);

        }

        [TestMethod]
        public void When_3_Dice_Are_Thrown_What_Is_The_Probability_Of_A_Combination_Of_Two_Different_Symbols()
        {
            const long n = 150000;
            long x = 0;
            int d = 3;
            for (int i = 0; i < n; i++)
            {
                var dices = DiceThrower.Throw(d);

                if (dices.HasAtLeast("A", 1) && dices.HasAtLeast("B", 1))
                {
                    x++;
                }
            }

            double p_sim = (double)x / n;

            Console.WriteLine("Probability simulated {0:0.00}%", p_sim * 100);

            var f = ChanceCalculator.GetProbabilityForLine("AB", 3);
            double p_formula = f.ToDouble();
            Console.WriteLine("Probability formula {0:0.00}%", p_formula * 100);

            double difference = Math.Abs(p_sim - p_formula);
            double rel_dif = difference / p_formula;
            Console.WriteLine("Relative difference {0:0.00}%", rel_dif * 100);

            Assert.IsTrue(Math.Abs(rel_dif) < 0.05);

        }
    }
}