using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class GameEngine
    {
        private const int number_of_cards = 14;
        private readonly CardSet m_cards;
        private readonly IList<Player> m_players = new List<Player>();
        private int m_current_player = 0;
        private int m_turns = 0;
        private int m_nr_dices = 7;
        private string m_name_of_played_card;

        public string CurrentPlayer { get { return m_players[m_current_player].Name; } }
        public int NumberOfTurns {  get { return m_turns; } }
        public IList<Player> Players { get { update_score();  return m_players;  } }

        public int NumberOfDices { get { return m_nr_dices; } }

        public GameEngine(CardSet cards)
        {
            m_current_player = 0;
            m_cards = cards;
        }

        // todo player has strategy
        public void AddPlayer(string name, CardChooser.Method method)
        {
            var player = new Player(name, method);
            m_players.Add(player);
        }

        public CardChooser GetCardChooser(string name)
        {
            var player = GetPlayerByName(name);
            if (player != null)
            {
                return new CardChooser(player.CardChooseMethod);
            }
            return new CardChooser();
        }

        private Player GetPlayerByName(string name)
        {
            return m_players.FirstOrDefault(x => x.Name == name, null);
        }

        public void NextPlayer()
        {
            m_current_player++;
            if (m_current_player >= m_players.Count)
            {
                m_current_player = 0;
            }
            m_turns++;
            m_nr_dices = 7;
        }

        public bool IsFinished()
        {
            return (m_cards.NumberOfCardsOnTable() == 0);
        }

        private void update_score()
        {
            foreach(var player in m_players)
            {
                int score = 0;

                foreach(var card in m_cards)
                {
                    if ( card.Owner == player.Name)
                    {
                        score += card.Points;

                        // todo group points..
                    }
                }

                player.SetScore(score);
            }
        }

        public bool ThrowFirstDiceOfTurn(DiceSet dices)
        {
            var match = new DiceMatch(dices, m_cards, this.CurrentPlayer);

            var algo = GetCardChooser(this.CurrentPlayer);
            var chosen_card = algo.ChooseCard(match);
            if (chosen_card == null)
            {
                chosen_card = algo.ChooseNonMatchingCard(m_cards);
            }

            m_name_of_played_card = chosen_card.CardName;
            var card = m_cards.GetCardByName(m_name_of_played_card);
            card.StartPlaying(stealing: !card.OnTable());
            var line = card.GetFreeLine(chosen_card.Line);
            if (line != null)
            {
                line.SetOccupied();
            }

            if (card.IsFull())
            {
                card.ChangeOwner(this.CurrentPlayer);
                return true;
            }

            m_nr_dices -= chosen_card.NumberOfDices;

            return false;
        }

        public bool ThrowNextDiceOfTurn(DiceSet dices)
        {
            var card = m_cards.GetCardByName(m_name_of_played_card);
            var match = new DiceMatch(dices, card, this.CurrentPlayer);

            bool card_won = false;

            if (match.HasMatches())
            {
                var algo = GetCardChooser(this.CurrentPlayer);
                var chosen_card = algo.ChooseCard(match);
                var line = card.GetFreeLine(chosen_card.Line);
                line.SetOccupied();

                m_nr_dices -= chosen_card.NumberOfDices;

                if (card.IsFull())
                {
                    card.ChangeOwner(this.CurrentPlayer);
                    return true;
                }
            }
            else
            {
                m_nr_dices--;
            }
            
            if (m_nr_dices <= 0 )
            {
                return true; 
            }
            return false;
        }

        public void StartPlayingCard(GameCard card)
        {
            m_name_of_played_card = card.Name;
            bool on_table = card.OnTable();
            card.StartPlaying(stealing: !on_table);
        }

        public void StopPlayingCard(GameCard card)
        {
            m_name_of_played_card = "";
            card.SetFree();
        }
    }
}