using SearchAlgorythms.ButtonExtension;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SearchAlgorythms.Top
{
    public class GraphTop : Button
    {
        private List<GraphTop> neighbours 
            = new List<GraphTop>();
      
        public GraphTop() : base()
        {
            SetToDefault();
        }

        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool IsVisited { get; set; }
        public int Value { get; set; }
        public GraphTop ParentTop { get; set; }

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
            this.MarkAsGraphTop();
            ParentTop = null;            
        }

        public bool IsSimpleTop()
        {
            return !IsStart && !IsEnd;
        }

        public void AddNeighbour(GraphTop top)
        {
            neighbours.Add(top);
        }
    }
}
