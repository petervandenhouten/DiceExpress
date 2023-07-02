using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class GameCardLine 
    {
        private string m_symbols;
        private bool m_free;

        // AA
        // AB
        // C
        // 10
        // 3

        public int Length {  get { return m_symbols.Length; } }

        public GameCardLine(string line) 
        {
            m_symbols = line;
            m_free = true;
        }

        public override string ToString()
        {
            return m_symbols;
        }

        public bool IsFree()
        {
            return m_free;
        }

        public void SetOccupied()
        {
            m_free = false;
        }

        public void SetFree()
        {
            m_free = true;
        }

        public string GetSymbol(int index, bool stealing)
        {
            if (index < 0 || index >= Length) return string.Empty;
            char symbol = m_symbols[index];
            if (stealing && symbol == 'D')
            {
                symbol = 'C';
            }
            return symbol.ToString();
        }

        public bool HasSymbols()
        {
            return !HasNumbers();
        }
        public bool HasNumbers()
        {
            return ToString().All(char.IsDigit);
        }

        public int GetPoints()
        {
            if (HasSymbols()) return 0;
            return int.Parse(ToString());
        }

        public bool HasIdenticalSymbols()
        {
            return (m_symbols[0] == m_symbols[1]);
        }

        public string ToConvertedString(bool stealing)
        {
            return stealing ? m_symbols.Replace('D', 'C') : m_symbols.Replace("D", "");

        }
    }
}
