using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class ReverseVertexConditions : IVerticesCommands
    {
        public ReverseVertexConditions()
        {
            conditions = new IVertexCommand[]
            {
                new SetVertexAsObstacleCondition(),
                new SetVertexAsRegularCondition()
            };
        }

        public void ExecuteTheFirstTrue(IVertex vertex)
        {
            if (!vertex.IsNull())
            {
                conditions
                  .FirstOrDefault(condition => condition.IsTrue(vertex))
                  ?.Execute(vertex);
            }
        }

        public void ResetAllExecutings()
        {

        }

        private readonly IVertexCommand[] conditions;
    }
}
