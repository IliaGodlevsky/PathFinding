using Common.Extensions;
using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsCondition.Realizations;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.EndPointsCondition
{
    internal sealed class EndPointsConditions
    {
        public EndPointsConditions(BaseEndPoints endPoints)
        {
            unsetSourceVertexCondition = new UnsetSourceVertexCondition(endPoints);
            unsetTargetVertexCondition = new UnsetTargetVertexCondition(endPoints);
            unsetIntermediateVertexCondition = new UnsetIntermediateVertexCondition(endPoints);
            conditions = new[]
            {
                unsetSourceVertexCondition,
                unsetTargetVertexCondition,
                unsetIntermediateVertexCondition,
                new ReplaceIntermediateVertexCondition(endPoints),
                new SetSourceVertexCondition(endPoints),
                new ReplaceSourceVertexCondition(endPoints),
                new SetTargetVertexCondition(endPoints),
                new ReplaceTargetVertexCondition(endPoints),
                new SetIntermediateVertexCondition(endPoints)
            };
            this.endPoints = endPoints;
        }

        public void Reset()
        {
            unsetSourceVertexCondition.Execute(endPoints.Source);
            unsetTargetVertexCondition.Execute(endPoints.Target);
            endPoints.IntermediateVertices.ForEach(unsetIntermediateVertexCondition.Execute);
        }

        public void ExecuteTheFirstTrue(IVertex vertex)
        {
            bool IsTrue(IEndPointsCondition condition)
            {
                return condition.IsTrue(vertex);
            }

            if (!vertex.IsNull() && !vertex.IsIsolated())
            {
                conditions
                    .FirstOrDefault(IsTrue)
                    ?.Execute(vertex);
            }
        }

        private readonly IEndPointsCondition unsetSourceVertexCondition;
        private readonly IEndPointsCondition unsetTargetVertexCondition;
        private readonly IEndPointsCondition unsetIntermediateVertexCondition;

        private readonly IIntermediateEndPoints endPoints;
        private readonly IEnumerable<IEndPointsCondition> conditions;
    }
}