using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IEndPoints
    {
        IVertex Target { get; }

        IVertex Source { get; }

        IEnumerable<IVertex> EndPoints { get; }
    }
}
