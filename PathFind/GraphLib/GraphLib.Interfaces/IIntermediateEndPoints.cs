using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IIntermediateEndPoints : IEndPoints
    {
        IReadOnlyCollection<IVertex> IntermediateVertices { get; }
    }
}
