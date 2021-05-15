using GraphLib.Common.NullObjects;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using NullObject.Attributes;

namespace GraphLib.Realizations.Factories.GraphAssembles
{
    [Null]
    public sealed class NullGraphAssemble : IGraphAssemble
    {
        public IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes)
        {
            return new NullGraph();
        }
    }
}
