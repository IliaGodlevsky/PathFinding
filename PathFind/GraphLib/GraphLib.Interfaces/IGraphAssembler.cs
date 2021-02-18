using System;
using System.Threading.Tasks;

namespace GraphLib.Interface
{
    public interface IGraphAssembler
    {
        event Action<Exception> OnExceptionCaught;

        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);

        Task<IGraph> AssembleGraphAsync(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
