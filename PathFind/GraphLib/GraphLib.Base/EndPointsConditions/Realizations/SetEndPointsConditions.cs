﻿using Common.Extensions;
using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsCondition.Realizations.LeftButtonConditions;
using GraphLib.Base.EndPointsConditions.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.EndPointsConditions.Realizations
{
    internal sealed class SetEndPointsConditions : IEndPointsConditions
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
            endPoints.IntermediateVertices.ForEach(unsetIntermediateVertexCondition.Execute);
        }

        public void ExecuteTheFirstTrue(IVertex vertex)
        {
            if (!vertex.IsNull() && !vertex.IsIsolated())
            {
                conditions.FirstOrDefault(condition => condition.IsTrue(vertex))?.Execute(vertex);
            }
        }

        private readonly IEndPointsCondition unsetSourceVertexCondition;
        private readonly IEndPointsCondition unsetTargetVertexCondition;
        private readonly IEndPointsCondition unsetIntermediateVertexCondition;
        private readonly BaseEndPoints endPoints;
        private readonly IEnumerable<IEndPointsCondition> conditions;
    }
}