using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IPathfindingRange : IEnumerable<IVertex>
    {
        IVertex Target { get; }

        IVertex Source { get; }
    }
}
