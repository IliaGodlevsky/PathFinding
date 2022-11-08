using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Visualization.Interfaces
{
    internal interface IVisualizationStore<TVertex>
        where TVertex : IVisualizable
    {
        IReadOnlyCollection<TVertex> GetVertices(IAlgorithm<IGraphPath> algorithm);
    }
}
