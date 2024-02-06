using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal abstract class VisualizationUnit : IVisualizationUnit
    {
        protected readonly AlgorithmReadDto algorithm;

        protected VisualizationUnit(AlgorithmReadDto algorithm)
        {
            this.algorithm = algorithm;
        }

        public abstract void Visualize(IGraph<Vertex> graph);
    }
}
