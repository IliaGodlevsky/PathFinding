using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Factory.Interface
{
    public interface IAlgorithmFactory<out TAlgorithm>
        where TAlgorithm : IAlgorithm<IGraphPath>
    {
        TAlgorithm Create(IPathfindingRange pathfindingRange);
    }
}