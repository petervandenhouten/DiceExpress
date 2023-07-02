using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class DiceThrower
    {
        static public DiceSet Throw(int n)
        {
            var set = new DiceSet(n);
            foreach(var dice in set)
            {
                dice.Throw();
            }
            return set;
        }
    }
}
