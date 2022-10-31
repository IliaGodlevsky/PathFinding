using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class IntermediateVertices<TVertex> : ResultVertices<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsIntermediate();
        }
    }
}
