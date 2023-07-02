using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class Player
    {
        public enum Strategy { Dynamic, Fixed };

        private string m_name;
        private int m_score;

        public string Name { get { return m_name; } }  
        public int Score { get { return m_score; }}

        
        public Player(string name)
        {
            m_name = name;
        }

        public void SetScore(int score)
        {
            m_score = score;
        }
    }
}
