using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EndPoints.Commands.ReplaceIntermediatesConditions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.VerticesConditions
{
    internal sealed class MarkIntermediateToReplaceEndPointsConditions : IVerticesCommands
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

        private readonly IVertexCommand cancelMarkToReplace;
        private readonly IVertexCommand[] conditions;
        private readonly BaseEndPoints endPoints;
    }
}
