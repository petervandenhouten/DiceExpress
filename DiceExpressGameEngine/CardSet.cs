using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class CardSet : List<GameCard>
    {
        private readonly Dictionary<int, int> m_group_score = new Dictionary<int, int>();

        public GameCard GetCardByName(string cardName)
        {
            return this.FirstOrDefault(x => x.Name == cardName, null);
        }

        public void SetPointsForGroup(int group, int points)
        {
            if (m_group_score.ContainsKey(group))
            {
                m_group_score[group] = points;
            }
            m_group_score.Add(group, points);
        }

        public int GetPointsForGroup(int group)
        {
            if (m_group_score.ContainsKey(group))
            {
                return m_group_score[group];
            }
            return 0;
        }

        public int NumberOfCardsOnTable()
        {
            return this.Count(x => string.IsNullOrEmpty(x.Owner));
        }
    }
}
