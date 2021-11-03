using GraphLib.Interfaces;
using GraphViewModel.Interfaces;

namespace GraphViewModel.Visualizations
{
    internal sealed class VisitedVertices : AlgorithmVertices, IProcessedVertices, IVisualization
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsVisited();
        }
    }
}
