using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.VerticesConditions
{
    internal sealed class ReturnColorsConditions : IVerticesCommands
    {
        public ReturnColorsConditions(BaseEndPoints endPoints)
        {
            conditions = new IVertexCommand[]
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

        private readonly IEnumerable<IVertexCommand> conditions;
    }
}
