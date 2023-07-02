using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class CardFactory
    {
        static public CardSet DefaultSet()
        {
            var set = new CardSet();

            // 1 Africa
            set.Add(new GameCard("South-West Africa", 2, 1, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("C"), new GameCardLine("8") }));
            set.Add(new GameCard("North-East Africa", 2, 1, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("AA"), new GameCardLine("5"), new GameCardLine("2") }));
            // 2. Asia
            set.Add(new GameCard("Russia", 2, 2, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("B"), new GameCardLine("B"), new GameCardLine("7") }));
            set.Add(new GameCard("China", 3, 2, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("B"), new GameCardLine("AA"), new GameCardLine("5") }));
            set.Add(new GameCard("Japan", 1, 2, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("B"), new GameCardLine("A"), new GameCardLine("C") }));
            set.Add(new GameCard("Middle-East", 1, 2, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("10") }));
            // 3. Australia
            set.Add(new GameCard("Australia", 3, 3, new List<GameCardLine> { new GameCardLine("B"), new GameCardLine("CC"), new GameCardLine("A"), new GameCardLine("4") }));
            // 4. North America
            set.Add(new GameCard("Canada", 2, 4, new List<GameCardLine> { new GameCardLine("DC"), new GameCardLine("B"), new GameCardLine("A"), new GameCardLine("3") }));
            set.Add(new GameCard("Mexico", 1, 4, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("C"), new GameCardLine("BB") }));
            set.Add(new GameCard("United States", 3, 4, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("AB"), new GameCardLine("AB"), new GameCardLine("3") }));
            // 5. South America
            set.Add(new GameCard("Brazil", 2, 5, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("C"), new GameCardLine("4"), new GameCardLine("4") }));
            set.Add(new GameCard("Peru", 1, 5, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("CC"), new GameCardLine("A") }));
            // 6. Europa
            set.Add(new GameCard("Western Europe", 4, 6, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("BB"), new GameCardLine("AA") }));
            set.Add(new GameCard("Eastern Europa", 3, 6, new List<GameCardLine> { new GameCardLine("D"), new GameCardLine("BA"), new GameCardLine("C"), new GameCardLine("6") }));

            set.SetPointsForGroup(1, 5);
            set.SetPointsForGroup(2, 10);
            set.SetPointsForGroup(3, 3);
            set.SetPointsForGroup(4, 8);
            set.SetPointsForGroup(5, 4);
            set.SetPointsForGroup(6, 8);

            // add group names?

            return set;
        }

        static public CardSet RandomSet(int nrcards)
        {
            var set = new CardSet();
            return set;
        }

        static public CardSet SingleCard()
        {
            var set = new CardSet();

            var lines = new List<GameCardLine>
            {
                new GameCardLine("AA"),
                new GameCardLine("BB"),
                new GameCardLine("3")
            };

            var card = new GameCard("Single", 4,0,lines);

            set.Add(card);

            return set;
        }
    }
}
