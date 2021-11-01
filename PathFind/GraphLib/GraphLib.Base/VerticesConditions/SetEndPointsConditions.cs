using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.VertexCondition.EndPointsConditions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.VerticesConditions
{
    internal sealed class SetEndPointsConditions : IVerticesConditions
    {
        public SetEndPointsConditions(BaseEndPoints endPoints)
        {
            unsetSourceVertexCondition = new UnsetSourceVertexCondition(endPoints);
            unsetTargetVertexCondition = new UnsetTargetVertexCondition(endPoints);
            unsetIntermediateVertexCondition = new UnsetIntermediateVertexCondition(endPoints);
            conditions = new[]
            {
                unsetSourceVertexCondition,
                unsetTargetVertexCondition,
                unsetIntermediateVertexCondition,
                new ReplaceIntermediateIsolatedVertexCondition(endPoints),
                new SetSourceVertexCondition(endPoints),
                new ReplaceIsolatedSourceVertexCondition(endPoints),
                new SetTargetVertexCondition(endPoints),
                new ReplaceIsolatedTargetVertexCondition(endPoints),
                new ReplaceIntermediateVertexCondition(endPoints),
                new SetIntermediateVertexCondition(endPoints)
            };
            this.endPoints = endPoints;
        }

        public void ResetAllExecutings()
        {
            unsetSourceVertexCondition.Execute(endPoints.Source);
            unsetTargetVertexCondition.Execute(endPoints.Target);
            var intermediateVertices = endPoints.IntermediateVertices.ToArray();
            intermediateVertices.ForEach(unsetIntermediateVertexCondition.Execute);
        }

        public void ExecuteTheFirstTrue(IVertex vertex)
        {
            if (!vertex.IsNull() && !vertex.IsIsolated())
            {
                conditions.FirstOrDefault(condition => condition.IsTrue(vertex))?.Execute(vertex);
            }
        }

        private readonly IVertexCondition unsetSourceVertexCondition;
        private readonly IVertexCondition unsetTargetVertexCondition;
        private readonly IVertexCondition unsetIntermediateVertexCondition;
        private readonly BaseEndPoints endPoints;
        private readonly IVertexCondition[] conditions;
    }
}