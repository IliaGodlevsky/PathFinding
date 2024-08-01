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
