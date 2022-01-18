using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// Reprsents a field for displaying graph
    /// </summary>
    public interface IGraphField
    {
        IReadOnlyCollection<IVertex> Vertices { get; }
    }
}
