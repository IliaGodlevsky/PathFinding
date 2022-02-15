using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseIntermediateEndPointsCommand : BaseEndPointsCommand
    {
        protected BaseIntermediateEndPointsCommand(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        protected bool HasIsolatedIntermediates => endPoints.intermediates.Any(vertex => vertex.IsIsolated());

        protected bool IsIntermediate(IVertex vertex)
        {
            return endPoints.intermediates.Contains(vertex);
        }
    }
}
