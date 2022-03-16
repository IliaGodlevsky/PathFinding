using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class VisitedVertices : AlgorithmVertices
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsVisited();
        }
    }
}
