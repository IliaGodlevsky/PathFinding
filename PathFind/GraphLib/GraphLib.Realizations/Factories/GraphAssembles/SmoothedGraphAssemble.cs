using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Extensions;
using GraphLib.Realizations.MeanCosts;
using GraphLib.Realizations.SmoothLevel;
using System.ComponentModel;

namespace GraphLib.Realizations.Factories.GraphAssembles
{
    [Description("Smoothed cost graph assemble")]
    public class SmoothedGraphAssemble : IGraphAssemble
    {
        public SmoothedGraphAssemble(
           IGraphAssemble graphAssemble,
           IVertexCostFactory costFactory,
           IMeanCost averageCost,
           ISmoothLevel smoothLevel)
        {
            this.graphAssemble = graphAssemble;
            this.costFactory = costFactory;
            this.averageCost = averageCost;
            this.smoothLevel = smoothLevel;
        }

        public SmoothedGraphAssemble(
           IGraphAssemble graphAssemble,
           IVertexCostFactory costFactory)
            : this(graphAssemble, costFactory,
                  new MeanCost(), new LowSmoothLevel())
        {

        }

        public SmoothedGraphAssemble(
            IGraphAssemble graphAssemble,
            IVertexCostFactory costFactory,
            IMeanCost meanCost)
            : this(graphAssemble, costFactory,
                  meanCost, new LowSmoothLevel())
        {

        }

        public SmoothedGraphAssemble(
            IGraphAssemble graphAssemble,
            IVertexCostFactory costFactory,
            ISmoothLevel smoothLevel)
            : this(graphAssemble, costFactory,
                  new MeanCost(), smoothLevel)
        {

        }

        public IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes)
        {
            return graphAssemble
                .AssembleGraph(obstaclePercent, graphDimensionSizes)
                .Smooth(costFactory, averageCost, smoothLevel);
        }

        private readonly IGraphAssemble graphAssemble;
        private readonly IVertexCostFactory costFactory;
        private readonly IMeanCost averageCost;
        private readonly ISmoothLevel smoothLevel;
    }
}