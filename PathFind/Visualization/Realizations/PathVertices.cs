using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class PathVertices : ResultVertices
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsPath();
        }
    }
}
