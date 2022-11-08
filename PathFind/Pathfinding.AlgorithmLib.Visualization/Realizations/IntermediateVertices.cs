using Pathfinding.AlgorithmLib.Visualization.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Visualization.Realizations
{
    internal sealed class IntermediateVertices<TVertex> : GraphVertices<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsIntermediate();
        }
    }
}
