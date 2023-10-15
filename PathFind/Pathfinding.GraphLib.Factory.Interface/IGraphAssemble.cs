using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface IGraphAssemble<TVertex>
        where TVertex : IVertex
    {
        IGraph<TVertex> AssembleGraph(IReadOnlyList<int> graphDimensionsSizes);
    }
}
