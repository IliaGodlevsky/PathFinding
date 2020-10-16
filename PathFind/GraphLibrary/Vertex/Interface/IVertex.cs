using GraphLibrary.Attributes;
using GraphLibrary.Coordinates.Interface;
using GraphLibrary.DTO;
using System.Collections.Generic;

namespace GraphLibrary.Vertex.Interface
{
    /// <summary>
    /// Represents an object that can be used to find a path in a graph
    /// </summary>
    public interface IVertex
    {
        bool IsEnd { get; set; }

        [DtoMember]
        bool IsObstacle { get; set; }
        bool IsStart { get; set; }
        bool IsVisited { get; set; }

        [DtoMember]
        int Cost { get; set; }
        IList<IVertex> Neighbours { get; set; }
        IVertex ParentVertex { get; set; }
        double AccumulatedCost { get; set; }
        ICoordinate Position { get; set; }
        Dto<IVertex> Dto { get; }
        void MarkAsEnd();
        void MarkAsSimpleVertex();
        void MarkAsObstacle();
        void MarkAsPath();
        void MarkAsStart();
        void MarkAsVisited();
        void MarkAsEnqueued();
    }
}