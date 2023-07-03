using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DiceExpressGameEngine;

namespace ConsoleDebugger // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DiceExpress ConsoleDebugger");

            // DO NOT USE THE GAME ENGINE FOR THE LOGIC
            
            var set = CardFactory.DefaultSet();

            var game = new GameEngine(set);
            game.AddPlayer("Peter", CardChooser.Method.CardOnTable);
            game.AddPlayer("Atari", CardChooser.Method.CardOnTable);
            game.AddPlayer("Paul", CardChooser.Method.CardOnTable);

            bool stop = false;

            while (!stop)
            {
                PrintSet(set);

                Console.WriteLine("*** Turn: {0} ***", game.CurrentPlayer);
                int nrOfDices = 7;
                var dices = DiceThrower.Throw(nrOfDices);
                PrintDiceSet(dices);

                var match = new DiceMatch(dices, set, game.CurrentPlayer);
                // PrintDiceMatch(match);
                 
                var algo = game.GetCardChooser(game.CurrentPlayer);
                var chosen_card = algo.ChooseCard(match);
                if ( chosen_card == null )
                {
                    chosen_card = algo.ChooseNonMatchingCard(set);
                }
                PrintChooseScores(algo);

                playCard(game, set, chosen_card, dices);

                var key = Console.ReadKey();

                if ( key.Key == ConsoleKey.X || game.IsFinished())
                {
                    stop = true;
                }

                game.NextPlayer();
            }

            if ( game.IsFinished() ) 
            {
                Console.WriteLine("*** Game Finished ***");
                printScore(game, set);
            }
        }

        private static void printScore(GameEngine game, CardSet set)
        {
            foreach(var player in game.Players)
            {
                Console.WriteLine("{0} {1} pts", player.Name, player.Score);
            }
            Console.WriteLine("Remaining cards on table: {0}", set.NumberOfCardsOnTable());
        }

        private static void playCard(GameEngine game, CardSet set, ChosenCard selected, DiceSet first_dices)
        {
            bool card_won = false;
            bool card_lost = false;

            int nr_dices = first_dices.Count;

            var card = set.GetCardByName(selected.CardName);
            game.StartPlayingCard(card);

            Console.WriteLine("Chosen card: {0} {1} {2}", card.Name, selected.Line, (card.HasOwner() ? "(Steal attempt)" : ""));

            var line = card.GetFreeLine(selected.Line);
            if (line != null)
            {
                line.SetOccupied();
            }

            if (card.IsFull())
            {
                card.ChangeOwner(game.CurrentPlayer);
                Console.WriteLine("{0} wins {1}", game.CurrentPlayer, selected.CardName);
                card_won = true;
                printScore(game, set);
            }

            nr_dices -= selected.NumberOfDices;

            PrintCard(card);

            while ( !game.IsFinished() && !card_won && !card_lost)
            {
                var dices = DiceThrower.Throw(nr_dices);
                PrintDiceSet(dices);

                var match = new DiceMatch(dices, card, game.CurrentPlayer);
                // PrintDiceMatch(match);

                if (match.HasMatches())
                {
                    var algo = game.GetCardChooser(game.CurrentPlayer);
                    var chosen_card = algo.ChooseCard(match);
                    PrintChooseScores(algo);
                    Console.WriteLine("Chosen line: {0} -{1}", chosen_card.Line, chosen_card.NumberOfDices);

                    line = card.GetFreeLine(chosen_card.Line);
                    line.SetOccupied();

                    nr_dices -= chosen_card.NumberOfDices;
                    PrintCard(card);

                    if ( card.IsFull())
                    {
                        if (card.ChangeOwner(game.CurrentPlayer))
                        {
                            Console.WriteLine("{0} steals {1}", game.CurrentPlayer, chosen_card.CardName);
                        }
                        else
                        {
                            Console.WriteLine("{0} wins {1}", game.CurrentPlayer, chosen_card.CardName);
                        }
                        card_won = true;
                        printScore(game, set);
                    }
                }
                else
                {
                    Console.WriteLine("No match for dices.");
                    nr_dices--;
                }

                if ( nr_dices <= 0 && !card_won)
                {
                    Console.WriteLine("No card won.");
                    card_lost = true;
                }
            }

            game.StopPlayingCard(card);
        }

        private static void PrintCard(GameCard card)
        {
            Console.WriteLine(card.Name);
            foreach(var line in card.GetLines())
            {
                Console.WriteLine("[{0}]{1}", line.IsFree() ? " " : "X", line.ToString());
            }
        }

        private static void PrintChooseScores(CardChooser algo)
        {
            foreach (var score in algo.Scores.Take(5))
            {
                Console.WriteLine("{0} {1} {2:0.00}% {3}", score.Match.Card.Name, score.Match.Line, score.Match.Probabilty*100, score.Score);
            }
        }

        private static void PrintDiceMatch(DiceMatch match)
        {
            foreach(var entry in match.Matches)
            {
                Console.WriteLine("{0} - {1} {2:0.0}%", entry.Card.Name, entry.Line, entry.Probabilty * 100);
            }
        }

        private static void PrintDiceSet(DiceSet dices)
        {
            Console.WriteLine("Dices {0}: {1}", dices.Count, dices.ToString());
        }

        private static void PrintSet(CardSet set)
        {
            Console.WriteLine("Number of cards: {0}", set.Count);
            foreach(var card in set)
            {
                Console.WriteLine(card.ToString());
            }
        }
    }
}