using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories.Layers;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories
{
    internal static class IGraphAssembleExtensions
    {
        public static Graph2D<TestVertex> AssembleTestGraph2D(this IGraphAssemble<Graph2D<TestVertex>, TestVertex> self)
        {
            var dimensions = Constants.DimensionSizes2D;
            var layers = new List<ILayer<Graph2D<TestVertex>, TestVertex>>()
            {
                new ObstacleLayer(),
                new CostLayer(),
                new NeighborhoodLayer()
            };
            return self.AssembleGraph(layers, dimensions);
        }
    }
}
