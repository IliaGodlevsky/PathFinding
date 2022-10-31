using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class SourceVertices<TVertex> : EndPointsVertices<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsSource();
        }
    }
}
