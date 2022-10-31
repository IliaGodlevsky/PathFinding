using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class EnqueuedVertices<TVertex> : AlgorithmVertices<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsEnqueued();
        }
    }
}
