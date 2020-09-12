using GraphLibrary.DTO;
using System.Collections.Generic;

namespace GraphLibrary.Vertex.Interface
{
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public interface IVertex
    {
        bool IsEnd { get; set; }
        bool IsObstacle { get; set; }
        bool IsStart { get; set; }
        bool IsVisited { get; set; }
        int Cost { get; set; }
        List<IVertex> Neighbours { get; set; }
        IVertex ParentVertex { get; set; }
        double AccumulatedCost { get; set; }
        Position Position { get; set; }
        VertexDto Dto { get; }
        void MarkAsEnd();
        void MarkAsSimpleVertex();
        void MarkAsObstacle();
        void MarkAsPath();
        void MarkAsStart();
        void MarkAsVisited();
    }
}