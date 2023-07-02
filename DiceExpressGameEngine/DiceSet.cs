using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class DiceSet : List<Dice>
    {
        public DiceSet() : this(7)
        {

        }

        public DiceSet(int dices) : base(dices)
        {
            for (int i = 0; i < dices; i++)
            {
                this.Add(new Dice());
            }
        }

        public override string ToString()
        {
            string str = "";
            foreach (var dice in this)
            {
                str += dice.ToString();
            }
            return str;
        }

        public bool HasAtLeast(string symbol, int count_required)
        {
            int count = 0;
            foreach (var dice in this)
            {
                if ( dice.ToString() == symbol )
                {
                    count++;
                }
            }
            return ( count >= count_required);
        }

        internal int GetPoints()
        {
            return this.Sum(dice => dice.GetPoints());
        }
    }
}
