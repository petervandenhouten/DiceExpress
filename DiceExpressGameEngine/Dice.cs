using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class Dice
    {
        private const int Number_of_sides = 6;
        private string m_value;
        private readonly Random m_random = new Random((int)System.DateTime.Now.Ticks);
        public Dice()
        {
            m_value = getRandomValue();
        }

        public Dice(string value)
        {
            m_value = value;
        }

        public string Throw()
        {
            m_value = getRandomValue();
            return m_value;
        }
        private string getRandomValue()
        { 
            int side = m_random.Next(Number_of_sides);

            switch(side)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
                case 3: return "3";
                case 4: return "2";
                default:
                case 5: return "1";
            }
        }

        public override string ToString()
        {
            return m_value;
        }

        public int GetPoints()
        {
            int points = 0;
            int.TryParse(m_value, out points);
            return points;
        }

        public bool HasNumber()
        {
            return (GetPoints() > 0);
        }
    }
}
