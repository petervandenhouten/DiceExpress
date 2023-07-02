using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceExpressGameEngine
{
    public class GameCard
    {
        private readonly List<GameCardLine> m_lines;
        private int m_points;
        private int m_group_id;
        private string m_owner;
        private string m_name;
        private bool m_try_to_steal;
        
        public string Name {  get { return m_name; } }  
        public int Points {  get {  return m_points; } }
        public string Owner { get { return m_owner; } }
        public int Group { get { return m_group_id; } }

        public GameCard(string name, int points, int group, List<GameCardLine> lines)
        {
            m_name = name;
            m_points = points;
            m_group_id = group;
            m_lines = lines;
            m_owner = string.Empty;
        }

        public IList<GameCardLine> GetFreeLines()
        {
            var lines = new List<GameCardLine>();

            foreach(var line in m_lines )
            {
                if ( line.IsFree() )
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}[{2}]: {3} [{4}]", m_name, m_points, m_group_id, LinesToString(), m_owner);
        }

        public bool OnTable()
        {
            return string.IsNullOrEmpty(m_owner);
        }

        public bool ChangeOwner(string playername)
        {
            bool stolen = !string.IsNullOrEmpty(m_owner);
            m_owner = playername;
            return stolen;
        }

        private string LinesToString()
        {
            string str = "";
            if (m_lines != null)
            {
                foreach (var line in m_lines)
                {
                    str += line + ", ";
                }
            }
            return str;
        }

        public GameCardLine GetLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return null;
            return this.m_lines.First(x => x.ToString() == line);
        }

        public GameCardLine GetFreeLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return null;
            return this.m_lines.FirstOrDefault(x => (x.ToString() == line && x.IsFree()), null);
        }

        public IList<GameCardLine> GetLines()
        {
            return m_lines;
        }

        public void StartPlaying(bool stealing)
        {
            m_try_to_steal = stealing;
            foreach (var line in m_lines)
            {
                if (line.ToString().Contains("D") && !line.ToString().Contains("C") && !stealing)
                {
                    line.SetOccupied();
                }
                else
                {
                    line.SetFree();
                }
            }
        }

        public bool IsFull()
        {
            foreach (var line in m_lines)
            {
                if (line.IsFree()) return false;
            }
            return true;
        }

        public void SetFree()
        {
            m_try_to_steal = false;
            foreach (var line in m_lines)
            {
                line.SetFree();
            }
        }

        public bool HasOwner()
        {
            return !OnTable();
        }
    }
}
