namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class ApplyCostsVisualizationLayer : VisualizationLayer
    {
        public ApplyCostsVisualizationLayer(RunVisualizationDto algorithm)
            : base(algorithm)
        {

        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            graph.ApplyCosts(algorithm.GraphState.Costs);
        }
    }
}
