using GraphLib.Base.VertexCondition.EndPointsConditions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.VerticesConditions
{
    internal sealed class ReturnColorsConditions : IVerticesConditions
    {
        public ReturnColorsConditions(BaseEndPoints endPoints)
        {
            conditions = new IVertexCondition[]
            {
                new ReturnSourceColorCondition(endPoints),
                new ReturnTargetColorCondition(endPoints),
                new ReturnMarkedToReplaceColorCondition(endPoints),
                new ReturnIntermediateColorCondition(endPoints)
            };
        }

        public void ExecuteTheFirstTrue(IVertex vertex)
        {
            conditions.FirstOrDefault(condition => condition.IsTrue(vertex))?.Execute(vertex);
        }

        public void ResetAllExecutings()
        {

        }

        private readonly IEnumerable<IVertexCondition> conditions;
    }
}
