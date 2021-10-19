using GraphLib.Interfaces;
using System;

namespace Algorithm.Infrastructure
{
    public class EndPointsNotFromCurrentGraphException : Exception
    {
        public IGraph Graph { get; }

        public IEndPoints EndPoints { get; }

        public EndPointsNotFromCurrentGraphException(IGraph graph, IEndPoints endPoints, string message)
            : base(message)
        {
            Graph = graph;
            EndPoints = endPoints;
        }

        public EndPointsNotFromCurrentGraphException(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, $"The chosen end points don't belong to the graph")
        {

        }
    }
}