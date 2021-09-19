using Common.Extensions;
using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsCondition.Realizations.MiddleButtonConditions;
using GraphLib.Base.EndPointsConditions.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.EndPointsConditions.Realizations
{
    internal sealed class MarkIntermediateToReplaceEndPointsConditions : IEndPointsConditions
    {
        public MarkIntermediateToReplaceEndPointsConditions(BaseEndPoints endPoints)
        {
            cancelMarkToReplace = new CancelMarkToReplaceEndPointsConditions(endPoints);
            conditions = new[]
            {
                cancelMarkToReplace,
                new MarkToReplaceEndPointsCondition(endPoints)
            };
            this.endPoints = endPoints;
        }

        public void ExecuteTheFirstTrue(IVertex vertex)
        {
            if (!vertex.IsNull() && !vertex.IsIsolated())
            {
                conditions.FirstOrDefault(condition => condition.IsTrue(vertex))?.Execute(vertex);
            }
        }

        public void ResetAllExecutings()
        {
            endPoints.markedToReplaceIntermediates.ForEach(cancelMarkToReplace.Execute);
        }

        private readonly IEndPointsCondition cancelMarkToReplace;
        private readonly IEnumerable<IEndPointsCondition> conditions;
        private readonly BaseEndPoints endPoints;
    }
}
