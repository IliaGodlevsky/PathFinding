using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Factory.Extensions
{
    public static class IGraphAssembleExtensions
    {
        public static TGraph AssembleGraph<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self,
            IEnumerable<ILayer<TGraph, TVertex>> layers, IReadOnlyList<int> dimensionSizes)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            var graph = self.AssembleGraph(dimensionSizes);
            foreach (var layer in layers)
            {
                layer.Overlay(graph);
            }
            return graph;
        }

        public static TGraph AssembleGraph<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self,
            IEnumerable<ILayer<TGraph, TVertex>> layers, params int[] dimensionSizes)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return self.AssembleGraph(layers, (IReadOnlyList<int>)dimensionSizes);
        }

        public static async ValueTask<TGraph> AssembleGraphAsync<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self,
            IEnumerable<ILayer<TGraph, TVertex>> layers, params int[] dimensionSizes)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return await self.AssembleGraphAsync(layers, (IReadOnlyList<int>)dimensionSizes);
        }

        public static async ValueTask<TGraph> AssembleGraphAsync<TGraph, TVertex>(this IGraphAssemble<TGraph, TVertex> self,
            IEnumerable<ILayer<TGraph, TVertex>> layers, IReadOnlyList<int> dimensionSizes)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex
        {
            return await Task.Run(() => self.AssembleGraph(layers, dimensionSizes));
        }
    }
}