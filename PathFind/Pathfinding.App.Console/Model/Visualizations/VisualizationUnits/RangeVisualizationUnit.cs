using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Extensions;
using System;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class RangeVisualizationUnit : VisualizationUnit
    {
        public RangeVisualizationUnit(AlgorithmReadDto algorithm) : base(algorithm)
        {
        }

        public override void Visualize(IGraph<Vertex> graph)
        {
            algorithm.Range
                .Select(graph.Get)
                .Reverse()
                .VisualizeAsRange();

        }
    }
}
