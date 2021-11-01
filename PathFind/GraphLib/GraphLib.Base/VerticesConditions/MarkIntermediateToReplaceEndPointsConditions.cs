using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.VertexCondition.ReplaceIntermediatesConditions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.VerticesConditions
{
    internal sealed class MarkIntermediateToReplaceEndPointsConditions : IVerticesConditions
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
            var marked = endPoints.markedToReplaceIntermediates.ToArray();
            marked.ForEach(cancelMarkToReplace.Execute);
        }

        private readonly IVertexCondition cancelMarkToReplace;
        private readonly IVertexCondition[] conditions;
        private readonly BaseEndPoints endPoints;
    }
}
