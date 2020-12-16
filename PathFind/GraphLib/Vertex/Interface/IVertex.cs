using Common.Interfaces;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Vertex.Cost;
using System.Collections.Generic;

namespace GraphLib.Vertex.Interface
{
    /// <summary>
    /// Represents an object that can be used to find a path in a graph
    /// </summary>
    public interface IVertex : IDefault
    {
        bool IsEnd { get; set; }

        bool IsObstacle { get; set; }

        bool IsStart { get; set; }

        bool IsVisited { get; set; }

        VertexCost Cost { get; set; }

        IList<IVertex> Neighbours { get; set; }

        IVertex ParentVertex { get; set; }

        double AccumulatedCost { get; set; }

        ICoordinate Position { get; set; }

        void MarkAsEnd();

        void MarkAsSimpleVertex();

        void MarkAsObstacle();

        void MarkAsPath();

        void MarkAsStart();

        void MarkAsVisited();

        void MarkAsEnqueued();

        void MakeUnweighted();

        void MakeWeighted();
    }
}