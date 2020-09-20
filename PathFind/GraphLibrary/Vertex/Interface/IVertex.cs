using GraphLibrary.DTO;
using System.Collections.Generic;

namespace GraphLibrary.Vertex.Interface
{
    /// <summary>
    /// Cartesian coordinates of the vertex on the graph
    /// </summary>
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object pos)
        {
            if(pos is Position position)
                return position.X == X && position.Y == Y;
            return false;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }

    /// <summary>
    /// Represents an object that can be used to find a path in a graph
    /// </summary>
    public interface IVertex
    {
        bool IsEnd { get; set; }
        bool IsObstacle { get; set; }
        bool IsStart { get; set; }
        bool IsVisited { get; set; }
        int Cost { get; set; }
        IList<IVertex> Neighbours { get; set; }
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
        void MarkAsEnqueued();
    }
}