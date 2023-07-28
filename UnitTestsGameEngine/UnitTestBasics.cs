using DiceExpressGameEngine;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestBasics
    {
        [TestMethod]
        public void Factorial()
        {
            Assert.AreEqual(1, ChanceCalculator.Factorial(0));
            Assert.AreEqual(1, ChanceCalculator.Factorial(1));
            Assert.AreEqual(2, ChanceCalculator.Factorial(2));
            Assert.AreEqual(6, ChanceCalculator.Factorial(3));
            Assert.AreEqual(24, ChanceCalculator.Factorial(4));
            Assert.AreEqual(120, ChanceCalculator.Factorial(5));
        }

        [TestMethod]
        public void Factorial_Overflow()
        {
            Assert.IsFalse(ChanceCalculator.Factorial(52) > 0); // gives overflow error
        }


        [TestMethod]
        public void Factorial_Division()
        {
            Assert.AreEqual(1, ChanceCalculator.FactorialDivision(0, 0));
            Assert.AreEqual(1, ChanceCalculator.FactorialDivision(0, 1));
            Assert.AreEqual(1, ChanceCalculator.FactorialDivision(1, 0));
            Assert.AreEqual(1, ChanceCalculator.FactorialDivision(1, 1));
            Assert.AreEqual(2, ChanceCalculator.FactorialDivision(2, 1));
            Assert.AreEqual(56, ChanceCalculator.FactorialDivision(8,6));
            Assert.AreEqual(24, ChanceCalculator.FactorialDivision(4, 1));
            Assert.AreEqual(14280, ChanceCalculator.FactorialDivision(120, 118));
        }

        [TestMethod]
        public void Permutations()
        {
            Assert.AreEqual(0, ChanceCalculator.NumberOfPermutations(0));
            Assert.AreEqual(1, ChanceCalculator.NumberOfPermutations(1));
            Assert.AreEqual(2, ChanceCalculator.NumberOfPermutations(2));
            Assert.AreEqual(6, ChanceCalculator.NumberOfPermutations(3));
            Assert.AreEqual(120, ChanceCalculator.NumberOfPermutations(5));
            Assert.AreEqual(720, ChanceCalculator.NumberOfPermutations(6));
            Assert.AreEqual(3628800, ChanceCalculator.NumberOfPermutations(10));
        }

        [TestMethod]
        public void PermutationsWithSelection()
        {
            Assert.AreEqual(0, ChanceCalculator.NumberOfPermutations(0, 0));
            Assert.AreEqual(336, ChanceCalculator.NumberOfPermutations(8, 3));
            Assert.AreEqual(210, ChanceCalculator.NumberOfPermutations(7, 3));
            Assert.AreEqual(5040, ChanceCalculator.NumberOfPermutations(10, 4));
        }

        [TestMethod]
        
        public void PermutationsForDices()
        {
            Assert.AreEqual(0, ChanceCalculator.NumberOfPermutationsForDices(0));
            //Assert.AreEqual(6, ChanceCalculator.NumberOfPermutationsForDices(1));
            Assert.AreEqual(36, ChanceCalculator.NumberOfPermutationsForDices(2));
        }

        [TestMethod]
        public void Combinations()
        {
            Assert.AreEqual(0, ChanceCalculator.NumberOfCombinations(0));
            Assert.AreEqual(1, ChanceCalculator.NumberOfCombinations(1));
            Assert.AreEqual(2, ChanceCalculator.NumberOfCombinations(2));
            Assert.AreEqual(6, ChanceCalculator.NumberOfCombinations(3));
        }

        [TestMethod]
        public void CombinationsWithSelection()
        {
            Assert.AreEqual(1, ChanceCalculator.NumberOfCombinations(2, 2));
            Assert.AreEqual(3, ChanceCalculator.NumberOfCombinations(3, 2));
            Assert.AreEqual(6, ChanceCalculator.NumberOfCombinations(6, 1));
            Assert.AreEqual(15, ChanceCalculator.NumberOfCombinations(6, 2));
            Assert.AreEqual(35, ChanceCalculator.NumberOfCombinations(7, 3));
            Assert.AreEqual(45, ChanceCalculator.NumberOfCombinations(10, 2));
            Assert.AreEqual(495, ChanceCalculator.NumberOfCombinations(12,4));
            Assert.AreEqual(2598960, ChanceCalculator.NumberOfCombinations(52,5));
        }

        [TestMethod]
        public void CombinationsForDices()
        {
            Assert.AreEqual(0, ChanceCalculator.NumberOfCombinationsForDices(0));
            Assert.AreEqual(6, ChanceCalculator.NumberOfCombinationsForDices(1));
            Assert.AreEqual(21, ChanceCalculator.NumberOfCombinationsForDices(2));
            Assert.AreEqual(56, ChanceCalculator.NumberOfCombinationsForDices(3));
            Assert.AreEqual(792, ChanceCalculator.NumberOfCombinationsForDices(7));
        }

        [TestMethod]
        public void PossibleOutcomeOfDices()
        {
            // The same as permutations.

            Assert.AreEqual(0, ChanceCalculator.NumberOfPossibleOutcomes(0));
            Assert.AreEqual(6, ChanceCalculator.NumberOfPossibleOutcomes(1));
            Assert.AreEqual(36, ChanceCalculator.NumberOfPossibleOutcomes(2));
            Assert.AreEqual(7776, ChanceCalculator.NumberOfPossibleOutcomes(5));
        }

    }
}