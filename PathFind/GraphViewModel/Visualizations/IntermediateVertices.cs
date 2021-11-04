using GraphLib.Interfaces;
using GraphViewModel.Interfaces;

namespace GraphViewModel.Visualizations
{
    internal sealed class IntermediateVertices : ResultVertices, IProcessedVertices, IVisualization
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsIntermediate();
        }
    }
}
