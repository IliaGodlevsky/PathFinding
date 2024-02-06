using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class VisualizationUnits : IVisualizationUnit
    {
        private readonly IReadOnlyCollection<IVisualizationUnit> units;

        public VisualizationUnits(AlgorithmReadDto algorithm)
        {
            units = new IVisualizationUnit[]
            {
                new RestoreVisualStateUnit(),
                new ApplyCostsVisualizationUnit(algorithm),
                new ObstaclesVisualizationUnit(algorithm),
                new RangeVisualizationUnit(algorithm),
                new SubAlgorithmVisualizationUnit(algorithm)
            }.ToReadOnly();
        }

        public void Visualize(IGraph<Vertex> graph)
        {
            foreach (var unit in units)
            {
                unit.Visualize(graph);
            }
        }
    }
}
