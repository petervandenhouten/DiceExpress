using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class DiceMatchEntry
    {
        public GameCard Card;
        public GameCardLine Line;
        public int NumberOfDices;
        public double Probabilty;
    }

    public class DiceMatch
    {
        private List<DiceMatchEntry> m_matching_lines = new List<DiceMatchEntry>();

        public DiceMatch(DiceSet dices, CardSet cards, string player)
        {
            foreach (var card in cards)
            {
                if (card.Owner != player)
                {
                    bool stealing = !string.IsNullOrEmpty(card.Owner);
                    CheckIfCardHasFreeLineForDices(card, dices, stealing);
                }
            }
            //if (!HasMatches())
            //{
            //    GetFreeCardFromTable(cards);
            //}
        }

        //private void GetFreeCardFromTable(CardSet cards)
        //{
        //    var card = cards.FirstOrDefault(x => x.OnTable() == true);

        //    AddMatchEntry(card, null, 0, 0);
        //}

        public DiceMatch(DiceSet dices, GameCard card, string player)
        {
            if (card != null && card.Owner != player)
            {
                CheckIfCardHasFreeLineForDices(card, dices, card.HasOwner());
            }
        }

        public IList<DiceMatchEntry> Matches { get { return m_matching_lines; } }

        public bool HasMatches()
        {
            return m_matching_lines.Count > 0;
        }

        private void CheckIfCardHasFreeLineForDices(GameCard card, DiceSet dices, bool stealing)
        {
            var lines = card.GetFreeLines();

            foreach(var line in lines)
            {
                if ( CheckIfDicesMatchWithLine(dices, line, stealing) )
                {
                    double p = GetProbabilityOfLine(line, dices.Count);
                    int d = GetNumberOfDicesRequired(line, dices);

                    AddMatchEntry(card, line, p, d);
                }
            }
        }

        private int GetNumberOfDicesRequired(GameCardLine line, DiceSet dices)
        {
            if (line.HasSymbols())
            {
                return line.Length;
            }
            else
            {
                return GetMinimumNumberDicesRequiredForNumbers(line, dices);
            }
        }

        private int GetMinimumNumberDicesRequiredForNumbers(GameCardLine line, DiceSet dices)
        {
            int point_of_line = line.GetPoints();

            // Dices with highest numbers first in list
            dices.Sort((a, b) => { return b.GetPoints().CompareTo(a.GetPoints()); });

            int sum = 0;
            int count = 0;
            foreach(var dice in dices)
            {
                if ( dice.HasNumber() )
                {
                    sum += dice.GetPoints();
                    count++;
                }
                if (sum >= point_of_line) break;
            }
            return count;
        }

        private double GetProbabilityOfLine(GameCardLine line, int count)
        {
            double p = 1;
            if (line.Length == 1)
            {
                //var f = ChanceCalculator.GetProbabilityForSymbolInAllowedThrows(count);
                var f = ChanceCalculator.GetProbabilityForSymbolWithMultipleDices(count);
                p = f.ToDouble();
            }
            else if (line.Length == 2)
            {
                //var f = ChanceCalculator.GetProbabilityForCombinationInAllowedThrows(count);
                var f = ChanceCalculator.GetProbabilityForCombinationWithMultipleDices(count);
                p = f.ToDouble();
            }

            return p;
        }

        private void AddMatchEntry(GameCard card, GameCardLine line, double probability, int numberOfDices)
        {
            m_matching_lines.Add(new DiceMatchEntry{Card = card, Line = line, Probabilty = probability, NumberOfDices = numberOfDices });
        }

        private bool CheckIfDicesMatchWithLine(DiceSet dices, GameCardLine line, bool stealing)
        {
            bool match = false;

            if ( line.HasSymbols() )
            {
                match = CheckMatchingSymbols(dices, line, stealing);
            }
            else
            {
                match = CheckMatchingNumbers(dices, line);
            }

            return match;
        }

        private bool CheckMatchingNumbers(DiceSet dices, GameCardLine line)
        {
            int points_of_line  = line.GetPoints();
            int points_of_dices = dices.GetPoints();

            return (points_of_dices >= points_of_line);
        }

        private bool CheckMatchingSymbols(DiceSet dices, GameCardLine line, bool stealing)
        {
            var dices_str = dices.ToString();
            //var line_str = line.ToConvertedString();

            // TODO, convert D into C
            // TODO Disable checks on D when not stealing

            int l = line.Length;
            bool match = true;

            for (int i = 0; i < l; i++)
            {
                var s = line.GetSymbol(i, stealing);

                int pos = dices_str.IndexOf(s);

                if (pos < 0)
                {
                    match = false;
                    break;
                }
                else
                {
                    dices_str = dices_str.Remove(pos, 1);
                }
;
            }

            return match;
        }
    }
}
