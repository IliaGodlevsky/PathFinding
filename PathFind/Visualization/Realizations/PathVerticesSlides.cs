using GraphLib.Interfaces;
using Visualization.Abstractions;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    internal sealed class PathVerticesSlides : AlgorithmResultSlides, IVisualization
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsPath();
        }
    }
}
