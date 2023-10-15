using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Factory.Extensions
{
    public static class IGraphAssembleExtensions
    {
        public static IGraph<TVertex> AssembleGraph<TVertex>(this IGraphAssemble<TVertex> self,
            IEnumerable<ILayer> layers, IReadOnlyList<int> dimensionSizes)
            where TVertex : IVertex
        {
            var graph = self.AssembleGraph(dimensionSizes);
            layers.ForEach(layer => layer.Overlay((IGraph<IVertex>)graph));
            return graph;
        }

        public static IGraph<TVertex> AssembleGraph<TVertex>(this IGraphAssemble<TVertex> self,
            IEnumerable<ILayer> layers, params int[] dimensionSizes)
            where TVertex : IVertex
        {
            return self.AssembleGraph(layers, (IReadOnlyList<int>)dimensionSizes);
        }

        public static async Task<IGraph<TVertex>> AssembleGraphAsync<TVertex>(this IGraphAssemble<TVertex> self,
            IEnumerable<ILayer> layers, params int[] dimensionSizes)
            where TVertex : IVertex
        {
            return await self.AssembleGraphAsync(layers, (IReadOnlyList<int>)dimensionSizes);
        }

        public static async Task<IGraph<TVertex>> AssembleGraphAsync<TVertex>(this IGraphAssemble<TVertex> self,
            IEnumerable<ILayer> layers, IReadOnlyList<int> dimensionSizes)
            where TVertex : IVertex
        {
            return await Task.Run(() => self.AssembleGraph(layers, dimensionSizes));
        }
    }
}