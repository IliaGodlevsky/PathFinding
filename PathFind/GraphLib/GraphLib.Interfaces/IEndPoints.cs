using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IEndPoints : IEnumerable<IVertex>
    {
        IVertex Target { get; }

        IVertex Source { get; }
    }
}
