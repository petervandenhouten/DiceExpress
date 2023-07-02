using System;
using DiceExpressGameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class SimulateGame
    {
        [TestMethod]
        public void When_NextPlayer_Then_NumberOfTurns_Increases()
        {
            var set = CardFactory.DefaultSet();

            var game = new GameEngine(set);
            game.AddPlayer("Peter", CardChooser.Method.CardPoints);
            game.AddPlayer("Atari", CardChooser.Method.EasyCard);
            game.AddPlayer("Paul", CardChooser.Method.CardPoints);

            Assert.AreEqual(0, game.NumberOfTurns);

            game.NextPlayer();
            Assert.AreEqual(1, game.NumberOfTurns);

            game.NextPlayer();
            Assert.AreEqual(2, game.NumberOfTurns);
        }

        [TestMethod]
        public void When_Game_Then_Finished_in_300_Turns()
        {
            var set = CardFactory.DefaultSet();

            var game = new GameEngine(set);
            game.AddPlayer("Peter", CardChooser.Method.CardPoints);
            game.AddPlayer("Atari", CardChooser.Method.EasyCard);
            game.AddPlayer("Paul", CardChooser.Method.CardPoints);

            bool stop = false;

            while (!stop)
            {
                var dices = DiceThrower.Throw(game.NumberOfDices);

                if ( !game.ThrowFirstDiceOfTurn(dices))
                {
                    dices = DiceThrower.Throw(game.NumberOfDices);

                    while ( !game.ThrowNextDiceOfTurn(dices))
                    {
                    }
                }

                Assert.IsTrue(game.NumberOfTurns < 300);

                game.NextPlayer();

                stop = game.IsFinished();
            }
        }
    }
}