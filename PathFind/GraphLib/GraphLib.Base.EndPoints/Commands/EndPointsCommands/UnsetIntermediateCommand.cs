using Commands.Attributes;
using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(2)]
    internal sealed class UnsetIntermediateCommand : BaseIntermediateEndPointsCommand
    {
        public UnsetIntermediateCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override bool CanExecute(IVertex vertex)
        {
            return IsIntermediate(vertex);
        }

        public override void Execute(IVertex vertex)
        {
            if (IsMarkedToReplace(vertex))
            {
                MarkedToReplace.Remove(vertex);
            }
            Intermediates.Remove(vertex);
            vertex.AsVisualizable().VisualizeAsRegular();
        }
    }
}
