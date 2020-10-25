using GraphLibrary.Attributes;
using GraphLibrary.Coordinates.Interface;
using GraphLibrary.Info;
using System.Collections.Generic;
using GraphLibrary.Vertex.Cost;

namespace GraphLibrary.Vertex.Interface
{
    /// <summary>
    /// Represents an object that can be used to find a path in a graph
    /// </summary>
    public interface IVertex
    {
        bool IsEnd { get; set; }
        [InfoMember]
        bool IsObstacle { get; set; }
        bool IsStart { get; set; }
        bool IsVisited { get; set; }
        [InfoMember]
        VertexCost Cost { get; set; }
        IList<IVertex> Neighbours { get; set; }
        IVertex ParentVertex { get; set; }
        double AccumulatedCost { get; set; }
        [InfoMember]
        ICoordinate Position { get; set; }
        Info<IVertex> Info { get; }
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