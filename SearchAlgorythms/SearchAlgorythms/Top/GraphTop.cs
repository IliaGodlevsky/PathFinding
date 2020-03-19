using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SearchAlgorythms.Top
{
    public class GraphTop : Button
    {
        private List<GraphTop> neighbours 
            = new List<GraphTop>();
      
        public GraphTop() : base()
        {
            ValueHistory = new List<int>();
            SetToDefault();
        }

        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool IsVisited { get; set; }
        public int Value { get; set; }
        public GraphTop ParentTop { get; set; }
        public List<int> ValueHistory { get; set; }
        public bool ValueChanged => ValueHistory.Last() != Value;

        public List<GraphTop> GetNeighbours()
        {
            return neighbours;
        }

        public void SetToDefault()
        {
            IsStart = false;
            IsEnd = false;
            IsVisited = false;
            Value = 0;
            BackColor = Color.FromName("White");
            ParentTop = null;
            ValueHistory.Clear();
            ValueHistory.Add(int.MaxValue);
        }

        public void AddNeighbour(GraphTop top)
        {
            neighbours.Add(top);
        }

        public void SaveValueInHistory() => ValueHistory.Add(Value);
    }
}
