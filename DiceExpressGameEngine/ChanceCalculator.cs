using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        static public Fraction GetProbabilityForLine(string str)
        {
            var line = new GameCardLine(str);
            return GetProbabilityForLine(line);
        }
        static public Fraction GetProbabilityForLine(GameCardLine line)
        {
            if (line.HasSymbols() && line.Length == 1)
            {
                return new Fraction(1, Sides);
            }
            else if (line.Length == 2 && !line.HasIdenticalSymbols())
            {
                return new Fraction(1, Sides * Sides);
            }

            // invalid
            return new Fraction();
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

            int den1 = Factorial(x);
            int den2 = Factorial(n - x);
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

        static public int Factorial(int f)
        {
            if (f==0)
            {
                return 1;
            }
            else
            {
                return f * Factorial(f - 1);
            }
        }
    }
}
