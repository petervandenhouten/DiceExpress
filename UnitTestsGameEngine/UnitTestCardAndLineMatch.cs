using System;
using System.IO;
using System.Text.RegularExpressions;
using DiceExpressGameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestCardAndLineMatch
    {
        [TestMethod]
        public void When_Two_Dices_Match_Double_Symbol()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("AA"),
                new GameCardLine("BB"),
                new GameCardLine("3")
            };

            var card = new GameCard("Test", 4, 1, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);

            var dice1 = new Dice("A");
            var dice2 = new Dice("A");
            var dice3 = new Dice("B");

            dices.Add(dice1);
            dices.Add(dice2);
            dices.Add(dice3);

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(     1, match.Matches.Count                 );
            Assert.AreEqual(     2, match.Matches[0].NumberOfDices      );
            Assert.AreEqual(  "AA", match.Matches[0].Line.ToString()    );
            Assert.AreEqual("Test", match.Matches[0].Card.Name          );
            Assert.AreEqual(     4, match.Matches[0].Card.Points        );
            Assert.AreEqual(     1, match.Matches[0].Card.Group         );
        }

        [TestMethod]
        public void When_Three_Dices_Match_Double_Symbol()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("AA"),
                new GameCardLine("BB"),
                new GameCardLine("3")
            };

            var card = new GameCard("Test", 4, 1, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);

            var dice1 = new Dice("A");
            var dice2 = new Dice("A");
            var dice3 = new Dice("A");

            dices.Add(dice1);
            dices.Add(dice2);
            dices.Add(dice3);

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(1, match.Matches.Count);
            Assert.AreEqual(2, match.Matches[0].NumberOfDices);
            Assert.AreEqual("AA", match.Matches[0].Line.ToString());
            Assert.AreEqual("Test", match.Matches[0].Card.Name);
            Assert.AreEqual(4, match.Matches[0].Card.Points);
            Assert.AreEqual(1, match.Matches[0].Card.Group);
        }

        [TestMethod]
        public void When_Dices_Do_Not_Match_Any_Line()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("AA"),
                new GameCardLine("BB"),
                new GameCardLine("3")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);

            var dice1 = new Dice("A");
            var dice2 = new Dice("B");
            var dice3 = new Dice("C");

            dices.Add(dice1);
            dices.Add(dice2);
            dices.Add(dice3);

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(0, match.Matches.Count);
        }

        [TestMethod]
        public void When_Two_Dices_Match_Exactly_With_Number()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("AA"),
                new GameCardLine("BB"),
                new GameCardLine("3")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);

            var dice1 = new Dice("2");
            var dice2 = new Dice("1");
            var dice3 = new Dice("B");

            dices.Add(dice1);
            dices.Add(dice2);
            dices.Add(dice3);

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(    1, match.Matches.Count              );
            Assert.AreEqual(    2, match.Matches[0].NumberOfDices   );
            Assert.AreEqual(  "3", match.Matches[0].Line.ToString() );
        }


        [TestMethod]
        public void When_One_Dice_Matches_Larger_Than_Number()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("AA"),
                new GameCardLine("BB"),
                new GameCardLine("2")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);

            var dice1 = new Dice("3");
            var dice2 = new Dice("1");
            var dice3 = new Dice("1");

            dices.Add(dice1);
            dices.Add(dice2);
            dices.Add(dice3);

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(   1, match.Matches.Count               );
            Assert.AreEqual(   1, match.Matches[0].NumberOfDices    );
            Assert.AreEqual( "2", match.Matches[0].Line.ToString()  );
        }

        [TestMethod]
        public void When_Three_Match_Exactly_With_Number()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("AA"),
                new GameCardLine("BB"),
                new GameCardLine("5")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);

            var dice1 = new Dice("3");
            var dice2 = new Dice("1");
            var dice3 = new Dice("1");

            dices.Add(dice1);
            dices.Add(dice2);
            dices.Add(dice3);

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(1, match.Matches.Count);
            Assert.AreEqual(3, match.Matches.First().NumberOfDices);
            Assert.AreEqual("5", match.Matches[0].Line.ToString());
        }

        [TestMethod]
        public void When_Two_Dices_Match_With_Duplicate_Lines()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("AB"),
                new GameCardLine("AB")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);

            var dice1 = new Dice("1");
            var dice2 = new Dice("A");
            var dice3 = new Dice("B");

            dices.Add(dice1);
            dices.Add(dice2);
            dices.Add(dice3);

            var match1 = new DiceMatch(dices, set, "player");
            Assert.AreEqual(2, match1.Matches.Count);
            Assert.AreEqual(2, match1.Matches.First().NumberOfDices);
            Assert.AreEqual("AB", match1.Matches[0].Line.ToString());

            var line1 = card.GetFreeLine(match1.Matches[0].Line.ToString());
            line1.SetOccupied();

            var match2 = new DiceMatch(dices, set, "player");
            Assert.AreEqual(1, match2.Matches.Count);
            Assert.AreEqual(2, match2.Matches.First().NumberOfDices);
            Assert.AreEqual("AB", match2.Matches[0].Line.ToString());

            var line2 = card.GetFreeLine(match2.Matches[0].Line.ToString());
            line2.SetOccupied();

            Assert.IsTrue(card.IsFull());
        }

        [TestMethod]
        public void When_One_Dice_Match_With_Symbol_To_Steal()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("D"),
                new GameCardLine("1"),
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);

            var dice1 = new Dice("A");
            var dice2 = new Dice("B");
            var dice3 = new Dice("C"); // matches with 'D'

            dices.Add(dice1);
            dices.Add(dice2);
            dices.Add(dice3);

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(1, match.Matches.Count);
            Assert.AreEqual(1, match.Matches.First().NumberOfDices);
            Assert.AreEqual("D", match.Matches[0].Line.ToString());
        }

        [TestMethod]
        public void When_Dices_Match_Card_With_D_BB_4()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("D"),
                new GameCardLine("BB"),
                new GameCardLine("4")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var throw1 = new DiceSet(0);
            throw1.Add(new Dice("C"));
            throw1.Add(new Dice("1"));
            throw1.Add(new Dice("1"));
                
            var match1 = new DiceMatch(throw1, set, "player");
            Assert.AreEqual(1, match1.Matches.Count);
            Assert.AreEqual(1, match1.Matches.First().NumberOfDices);
            Assert.AreEqual("D", match1.Matches[0].Line.ToString());

            var throw2 = new DiceSet(0);
            throw2.Add(new Dice("1"));
            throw2.Add(new Dice("B"));
            throw2.Add(new Dice("B"));

            var match2 = new DiceMatch(throw2, set, "player");
            Assert.AreEqual(1, match2.Matches.Count);
            Assert.AreEqual(2, match2.Matches.First().NumberOfDices);
            Assert.AreEqual("BB", match2.Matches[0].Line.ToString());

            var throw3 = new DiceSet(0);
            throw3.Add(new Dice("A"));
            throw3.Add(new Dice("2"));
            throw3.Add(new Dice("2"));

            var match3 = new DiceMatch(throw3, set, "player");
            Assert.AreEqual(1, match3.Matches.Count);
            Assert.AreEqual(2, match3.Matches.First().NumberOfDices);
            Assert.AreEqual("4", match3.Matches[0].Line.ToString());

        }

        [TestMethod]
        public void When_Dice_And_Stealing_Then_Match()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("DC"),
                new GameCardLine("B"),
                new GameCardLine("1")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);
            dices.Add(new Dice("C"));
            dices.Add(new Dice("C"));
            dices.Add(new Dice("A"));

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(1, match.Matches.Count);
            Assert.AreEqual(2, match.Matches.First().NumberOfDices);
            Assert.AreEqual("DC", match.Matches[0].Line.ToString());
        }

        [TestMethod]
        public void When_Dice_And_Stealing_Then_No_Match()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("DC"),
                new GameCardLine("B"),
                new GameCardLine("1")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);
            dices.Add(new Dice("C"));
            dices.Add(new Dice("A"));
            dices.Add(new Dice("A"));

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(0, match.Matches.Count);
        }

        [TestMethod]
        public void When_Two_Dices_And_Stealing_Then_Match()
        {
            var lines = new List<GameCardLine>
            {
                new GameCardLine("D"),
                new GameCardLine("C"),
                new GameCardLine("1")
            };

            var card = new GameCard("Test", 4, 0, lines);

            var set = new CardSet();
            set.Add(card);

            var dices = new DiceSet(0);
            dices.Add(new Dice("C"));
            dices.Add(new Dice("A"));
            dices.Add(new Dice("A"));

            var match = new DiceMatch(dices, set, "player");
            Assert.AreEqual(2, match.Matches.Count);
            Assert.AreEqual(1, match.Matches.First().NumberOfDices);
            Assert.AreEqual("D", match.Matches[0].Line.ToString());

            lines[0].SetOccupied();

            var match2 = new DiceMatch(dices, set, "player");
            Assert.AreEqual(1, match2.Matches.Count);
            Assert.AreEqual(1, match2.Matches.First().NumberOfDices);
            Assert.AreEqual("C", match2.Matches[0].Line.ToString());

        }

    }
}