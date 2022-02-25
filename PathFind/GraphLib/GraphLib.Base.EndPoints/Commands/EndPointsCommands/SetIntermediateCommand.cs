using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(9)]
    internal sealed class SetIntermediateCommand : BaseIntermediateEndPointsCommand
    {
        public SetIntermediateCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            Intermediates.Add(vertex);
            vertex.AsVisualizable().VisualizeAsIntermediate();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}
