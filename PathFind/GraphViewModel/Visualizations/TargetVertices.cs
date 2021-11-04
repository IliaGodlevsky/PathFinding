using GraphLib.Interfaces;
using GraphViewModel.Interfaces;

namespace GraphViewModel.Visualizations
{
    internal sealed class TargetVertices : EndPointsVertices, IProcessedVertices, IVisualization
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsTarget();
        }
    }
}
