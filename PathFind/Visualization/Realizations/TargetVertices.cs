using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class TargetVertices<TVertex> : EndPointsVertices<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsTarget();
        }
    }
}
