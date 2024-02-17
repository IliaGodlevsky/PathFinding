using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal abstract class VisualizationLayer : ILayer
    {
        protected readonly RunVisualizationDto algorithm;

        protected VisualizationLayer(RunVisualizationDto algorithm)
        {
            this.algorithm = algorithm;
        }

        public abstract void Overlay(IGraph<IVertex> graph);
    }
}
