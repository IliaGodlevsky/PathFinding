using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// An interface, that provides 
    /// methods and properties 
    /// for source and target vertices
    /// for pathfinding algorithm
    /// </summary>
    public interface IEndPoints
    {
        IVertex Target { get; }

        IVertex Source { get; }

        IEnumerable<IVertex> EndPoints { get; }

        bool IsEndPoint(IVertex vertex);
    }
}
