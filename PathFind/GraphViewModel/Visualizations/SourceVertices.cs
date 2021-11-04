using GraphLib.Interfaces;
using GraphViewModel.Interfaces;

namespace GraphViewModel.Visualizations
{
    internal sealed class SourceVertices : EndPointsVertices, IProcessedVertices, IVisualization
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsSource();
        }
    }
}
