using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IGraphField<out TVertex> 
        where TVertex : IVertex
    {
        IReadOnlyCollection<TVertex> Vertices { get; }
    }
}
