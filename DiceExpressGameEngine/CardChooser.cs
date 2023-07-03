using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class CardScore
    {
        public DiceMatchEntry Match;
        public double Score;
    }

    public class ChosenCard
    {
        public string CardName;
        public string Line;
        public int NumberOfDices;
    }

    public class CardChooser
    {
        private readonly Random m_random = new Random((int)DateTime.Now.Ticks);
        private readonly List<CardScore> m_scores = new List<CardScore>();
        private Method m_method;

        public enum Method { Human, GroupPoints, CardPoints, StealOfOpponents, EasyCard, CardOnTable}

        public IList<CardScore> Scores {  get { return m_scores; } }

        // todo pattern to different algorithms
        public CardChooser()
        {
            m_method = Method.CardPoints;
        }

        public CardChooser(Method method)
        {
            m_method = method;
        }

        public void ChangeMethod(Method method)
        {
            m_method = method;
        }

        public ChosenCard ChooseCard(DiceMatch match)
        {
            m_scores.Clear();

            if (match.Matches.Count == 0)
            {
                return null;
            }

            foreach(var entry  in match.Matches)
            {
                var score = CalculateScore(entry);
                m_scores.Add(new CardScore { Match = entry, Score = score });
            }

            m_scores.Sort((a, b) => { return b.Score.CompareTo(a.Score); });

            var best_entry = m_scores.MaxBy(x => x.Score);
            if ( best_entry == null )
            {
                best_entry = m_scores.First();
            }
            return new ChosenCard { CardName = best_entry.Match.Card.Name, Line = best_entry.Match.Line.ToString(), NumberOfDices = best_entry.Match.NumberOfDices };
        }

        private double CalculateScore(DiceMatchEntry entry)
        {
            switch( m_method )
            {
                case Method.EasyCard:
                    return GetScoreBasedOnEasyCardPoints(entry);

                case Method.CardOnTable:
                    return GetScoreBasedOnCardOnTable(entry);

                case Method.CardPoints:
                default:
                    return GetScoreBasedOnCardPoints(entry);
            }
        }

        private double GetScoreBasedOnCardOnTable(DiceMatchEntry entry)
        {
            double score = GetScoreBasedOnCardPoints(entry);
            if ( !entry.Card.OnTable() )
            {
                score = 0;
            }
            return score;
        }

        private double GetScoreBasedOnEasyCardPoints(DiceMatchEntry entry)
        {
            double card_points = entry.Card.Points * 100;
            double line_difficulty = 100 * entry.Probabilty;
            double random = m_random.NextDouble() * 10;

            return card_points + line_difficulty + random;
        }

        private double GetScoreBasedOnCardPoints(DiceMatchEntry entry)
        {
            double card_points = entry.Card.Points * 100;
            double line_difficulty = 100 / entry.Probabilty;
            double random = m_random.NextDouble() * 10;

            return card_points + line_difficulty + random;
        }

        public ChosenCard ChooseNonMatchingCard(CardSet set)
        {
            switch (m_method)
            {
                case Method.EasyCard:
                    return GetCardWithLeastPoints(set);

                case Method.GroupPoints:
                    return GetCardWithMostGroupPoints(set);

                case Method.CardPoints:
                default:
                    return GetRandomCard(set);
            }
        }

        private ChosenCard GetRandomCard(CardSet set)
        {
            return GetChosencard(set[m_random.Next(set.Count)]);
        }

        private ChosenCard GetCardWithMostGroupPoints(CardSet set)
        {
            set.Sort((a, b) => 
            {
                int pb = set.GetPointsForGroup(b.Group);
                int pa = set.GetPointsForGroup(a.Group);
                return pb.CompareTo(pa); 
            });

            return GetChosencard(set.First());
        }

        private ChosenCard GetCardWithLeastPoints(CardSet set)
        {
            set.Sort((a, b) => { return a.Points.CompareTo(b.Points); });
            return GetChosencard(set.First());
        }

        private ChosenCard GetChosencard(GameCard gameCard)
        {
            return new ChosenCard { CardName = gameCard.Name, Line = "", NumberOfDices = 0 };
        }

    }
}
