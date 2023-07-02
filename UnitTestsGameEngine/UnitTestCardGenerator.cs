using DiceExpressGameEngine;

namespace UnitTestsGameEngine
{
    [TestClass]
    public class UnitTestCardGenerator
    {
        [TestMethod]
        public void Create_Set_With_Single_Card()
        {
            var set = CardFactory.SingleCard();

            Assert.AreEqual(1, set.Count);

        }

        [TestMethod]
        public void Create_Set_With_Default_Card()
        {
            var set = CardFactory.DefaultSet();

            Assert.AreEqual(14, set.Count);
        }

        [TestMethod]
        public void Get_Card_By_Name()
        {
            var set = CardFactory.DefaultSet();

            Assert.AreEqual(14, set.Count);

            var card = set.GetCardByName("Canada");

            Assert.IsNotNull(card);
        }

        [TestMethod]
        public void Get_Card_Of_Unknown_Name()
        {
            var set = CardFactory.DefaultSet();

            Assert.AreEqual(14, set.Count);

            var card = set.GetCardByName("Hallo");

            Assert.IsNull(card);
        }

    }
}