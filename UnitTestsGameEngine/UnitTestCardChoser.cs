using System;
using DiceExpressGameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestCardChoser
    {
        [TestMethod]
        public void When_No_Match_Then_Card_With_Least_Points_Is_Chosen()
        {
            var set = CardFactory.DefaultSet();

            var choser = new CardChooser();
            choser.ChangeMethod(CardChooser.Method.EasyCard);

            var chosen = choser.ChooseNonMatchingCard(set);
            Assert.IsNotNull(chosen);

            var card = set.GetCardByName(chosen.CardName);
            Assert.IsNotNull(card);
            Assert.AreEqual(1, card.Points);

        }

        [TestMethod]
        public void When_No_Match_Then_Card_With_Most_Group_Points_Is_Chosen()
        {
            var set = CardFactory.DefaultSet();

            var choser = new CardChooser();
            choser.ChangeMethod(CardChooser.Method.GroupPoints);

            var chosen = choser.ChooseNonMatchingCard(set);
            Assert.IsNotNull(chosen);

            var card = set.GetCardByName(chosen.CardName);
            Assert.IsNotNull(card);

            int group_points = set.GetPointsForGroup(card.Group);
            Assert.AreEqual(10, group_points);

        }
    }
}