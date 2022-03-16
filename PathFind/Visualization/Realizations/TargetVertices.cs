using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class TargetVertices : EndPointsVertices
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsTarget();
        }
    }
}
