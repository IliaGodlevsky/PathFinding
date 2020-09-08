using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Drawing;

namespace GraphLibrary.Model.Vertex
{
    public class NullVertex : IVertex
    {
        private static NullVertex instance = null;
        public static NullVertex GetInstance()
        {
            if (instance == null)
                instance = new NullVertex();
            return instance;
        }

        private NullVertex()
        {
            isEnd = true;
            isObstacle = true;
            isStart = true;
            isVisited = true;
            cost = 0;
            neighbours = new List<IVertex>();
            accumulatedCost = double.PositiveInfinity;
            location = new Point(0, 0);
        }

        private bool isEnd;
        public bool IsEnd { get => isEnd; set => isEnd = true; }

        private bool isObstacle;
        public bool IsObstacle { get => isObstacle; set => isObstacle = true; }

        private bool isStart;
        public bool IsStart { get => isStart; set => isStart = true; }

        private bool isVisited;
        public bool IsVisited { get => isVisited; set => isVisited = false; }

        private int cost;
        public int Cost { get => cost; set => cost = 0; }

        private List<IVertex> neighbours;
        public List<IVertex> Neighbours { get => neighbours; set => neighbours = new List<IVertex>(); }

        private IVertex parentVertex;
        public IVertex ParentVertex { get => GetInstance(); set => parentVertex = GetInstance(); }

        private double accumulatedCost;
        public double AccumulatedCost { get => accumulatedCost; set => accumulatedCost = double.PositiveInfinity; }

        private Point location;
        public Point Location { get => location; set => location = new Point(0, 0); }

        public VertexInfo Info => new VertexInfo(this);

        public void MarkAsEnd(){}
        public void MarkAsObstacle() { }
        public void MarkAsPath() { }
        public void MarkAsSimpleVertex() { }
        public void MarkAsStart() { }
        public void MarkAsVisited() { }
    }
}
