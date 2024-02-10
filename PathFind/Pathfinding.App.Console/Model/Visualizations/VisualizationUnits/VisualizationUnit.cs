using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal abstract class VisualizationUnit : IVisualizationUnit
    {
        protected readonly RunVisualizationDto algorithm;

        protected VisualizationUnit(RunVisualizationDto algorithm)
        {
            this.algorithm = algorithm;
        }

        public abstract void Visualize(IGraph<Vertex> graph);
    }
}
