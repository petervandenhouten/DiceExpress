using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Mehroz;

namespace DiceExpressGameEngine
{
    public class ChanceCalculator
    {
        private const int Sides = 6;

        // P(X=r) = c * p^r * (1-p)^(n-r)
        // c = number of combinations
        // n = number of dices
        // p = change for a side (1/6)
        // r = number of success 

        static public long FactorialDivision(int topFactorial, int divisorFactorial)
        {
            long result = 1;
            for (int i = topFactorial; i > divisorFactorial; i--)
                result *= i;
            return result;
        }

        static public long NumberOfCombinations(int n)
        {
            if (n == 0) return 0;
            return Factorial(n);
        }

        /// <summary>
        /// Combinations: Unordered Sampling without Replacement:
        /// The number of k combinations of an n element.
        /// N = total number of objects
        /// k = number of objects selected/chosen at one time
        /// </summary>
        static public long NumberOfCombinations(int n, int k)
        {
            return n_over_k(n, k);
        }

        static public long n_over_k(int n, int k)
        {
            if (n == 0) return 0;
            return NumberOfPermutations(n, k) / Factorial(k);
        }

        static public long NumberOfPossibleOutcomes(int dices)
        {
            if (dices == 0) return 0;
            return OrderedSamplingWithReplacement(Sides, dices);
        }

        /// <summary>
        /// OrderedSamplingwithReplacement
        /// N = total number of elements
        /// k = number of samples
        /// </summary>
        static public long OrderedSamplingWithReplacement(int n, int k)
        {
            return (long)Math.Pow(n, k);
        }

        static public long NumberOfCombinationsForDices(int dices)
        {
            // n = dices + Sides - 1;
            // k = dices;
            return UnorderedSamplingWithReplacement(Sides, dices);
        }

        /// <summary>
        /// H = (k + n–1) over (k)
        /// where n is the number of objects (the possible outcomes)
        /// and k is the number of boxes (the number of rolls). 
        /// </summary>
        static public long UnorderedSamplingWithReplacement(int n, int k)
        {
            if (n == 0 || k ==0) return 0;
            return n_over_k(n + k - 1, k);
        }

        static public long NumberOfPermutationsForDices(int dices)
        {
            return NumberOfPossibleOutcomes(dices);
        }

        static public long NumberOfPermutations(int n)
        {
            if (n == 0) return 0;
            return Factorial(n);
        }

        /// <summary>
        /// Permutations: Ordered Sampling without Replacement
        /// Possible arrangements (orders) of n objects
        /// The number of k permutations of n distinguishable objects
        /// n=objects, k=selected/chosen
        /// </summary>
        static public long NumberOfPermutations(int n, int k)
        {
            if (n == 0) return 0;
            return FactorialDivision(n, n - k);
        }

        static public Fraction GetProbabilityForLine(string str, int dices)
        {
            var line = new GameCardLine(str);
            return GetProbabilityForLine(line,dices);
        }
        static public Fraction GetProbabilityForLine(GameCardLine line, int dices)
        {
            if (dices<1) return new Fraction(0);

            if (line.HasSymbols() && line.Length == 1)
            {
                // one symbol A
                // change of not throwing a A with one dice = 5/6
                // change of not throwing a A with two dices =  5/6 * 5/6
                // change of throwing at least one A with two dices = 1 - (5/6*5/6)

                // formula
                // 1 - (5/6)^n

                var prop_no_sym = new Fraction(5, Sides);
                var response1 = 1 - (prop_no_sym ^ dices);

                // with binomial formula, change of not throwing A in several attempts
                // N = dices
                // p = 1/6
                // x = exactly 0
                var response2 = 1 - BinomialDistribution(dices, 0, new Fraction(1, 6));

                return response1;
            }
            else if (line.Length == 2 && line.HasSymbols() && line.HasIdenticalSymbols())
            {
                var n = NumberOfCombinationsForDices(dices);
                var combinations_aa = OrderedSamplingWithReplacement(2, dices);
                return new Fraction(combinations_aa, n);

                // Two identical symbols AA
                // For 2 dices, 1 combination of 36 possibilities gives a success

                // N = dices
                // p = 1/6

                // x = minimum 2 times, exactly 2,3,4....N

                var p_total = new Fraction(0);
                for (int d=2; d<=dices; d++) // make this sum for loop a formula
                {
                    var p = BinomialDistribution(dices, d, new Fraction(1, 6));
                    p_total += p;
                }
                var response2 = p_total;
                var response3 = SummedBinomialDistribution(dices, 2, dices, new Fraction(1, 6));

                // inverse x = maximum 1 times, exact 0 or exact 1

                var p0 = BinomialDistribution(dices, 0, new Fraction(1, 6));
                var p1 = BinomialDistribution(dices, 1, new Fraction(1, 6));
                var response1 = 1 - (p0 + p1);

                return response1;
            }
            else if (line.Length == 2 && line.HasSymbols() &&  !line.HasIdenticalSymbols())
            {
                // 3 dices
                var n = NumberOfCombinationsForDices(dices);
                var nrSymbols = 2;
                var nxBoxes = dices;
                var combinations_ab = NumberOfCombinations(6, 2);

                return new Fraction(combinations_ab, n);
                // '-------------------'

                // Two diffrent symbols AB. It means AB and BA are valid.
                // For 2 dices, 2 combinations of 36 possibilities gives a success
                // For 3 dices, 3x(2*6) = 36? combinations of 216 possibilities gives a success

                // (1) We can calculate the propability for any combinates of A & B: AA, AB, BA, BB with n dices
                // (2) and subtract the propability of all A: AA
                // (3) and subtract the propability of all B: BB

                // (1) at least 2 out of N are A or B
                // N = dices
                // p = 2/6, either A OR B 
                // x = minimum 2 times, exactly 2,3,4,N
                var p_A_or_B_twice = SummedBinomialDistribution(dices, 2, dices, new Fraction(2,6));

                // Not A and Not B => 4/6
                var p_less_than_A_or_B_twice = SummedBinomialDistribution(dices, 0, 1, new Fraction(4, 6));
                var p_A_or_B_twice2 = new Fraction(dices * dices * 6, (int)Math.Pow(6, dices));

                // (2) all A
                // p = 1/6
                // x = dices
                var p_all_A = BinomialDistribution(dices, dices, new Fraction(1, 6));

                // (3)
                var p_all_B = p_all_A;

                var response = p_A_or_B_twice - (p_all_A + p_all_B);

                return response;
            }

            // invalid
            return new Fraction();
        }

        static public Fraction SummedBinomialDistribution(int n, int x1, int x2, Fraction p)
        {
            var p_total = new Fraction(0);
            for (int x = x1; x <= x2; x++)
            {
                var p_exact_x = BinomialDistribution(n, x, p);
                p_total += p_exact_x;
            }
            return p_total;
        }

        static public Fraction GetChanceForLine(string line, int dices)
        {
            // Sum of individual probabilities of symbols in line
            return new Fraction();
        }

        static public Fraction GetProbabilityForCombinationInAllowedThrows(int start_dices)
        {
            var f = new Fraction();

            for (int dices = start_dices; dices > 0; dices--)
            {
                var g = GetProbabilityForCombinationWithMultipleDices(dices);
                f = f + g;
            }

            return f;
        }

        static public Fraction GetProbabilityForCombinationWithMultipleDices(int dices)
        {
            if (dices < 2) return new Fraction();

            var total = new Fraction();

            // We add the probability of 2,3,4,5...dices times that the event will occur
            for (int i = 2; i <= dices; i++)
            {
                int x = i;          // must occurs eaxtly times for a combination
                int n = dices;      // number of trials
                Fraction p = new Fraction(1, 6);

                var p_x_is_i = BinomialDistribution(n, x, p);
                total += p_x_is_i;
            }
            return total;
        }

        static public Fraction BinomialDistribution(int n, int x, Fraction p)
        {
            // Calculate the probability that in n trials, and event with probablity p will occur exactly x times.
            Fraction num1 = Factorial(n);
            Fraction num2 = p ^ x;
            Fraction num3 = (1 - p) ^ (n - x);

            var den1 = Factorial(x);
            var den2 = Factorial(n - x);
            return (num1*num2*num3) / (den1*den2);
        }


        static public Fraction GetProbabilityForSymbolInAllowedThrows(int start_dices)
        {
            var f = new Fraction();

            for(int dices = start_dices; dices>0; dices--)
            {
                var g = GetProbabilityForSymbolWithMultipleDices(dices);
                f = f + g;
            }

            return f;
        }

        static public Fraction GetProbabilityForSymbolWithMultipleDices( int dices)
        {
            // probabiliy of not throwing a certain symbol with multiple dices
            var f = new Fraction(5,6) ^ dices;

            // the probability of at least one of the dices is a success
            var g = 1 - f;

            return g;
        }

        static public long Factorial(int f)
        {
            if (f==0)
            {
                return 1;
            }
            else
            {
                return (long)f * Factorial(f - 1);
            }
        }
    }
}
