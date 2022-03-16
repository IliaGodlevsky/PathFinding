using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class SourceVertices : EndPointsVertices
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsSource();
        }
    }
}
