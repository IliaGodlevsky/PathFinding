using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(4)]
    internal sealed class SetSourceCommand : BaseEndPointsCommand
    {
        public SetSourceCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override bool CanExecute(IVertex vertex)
        {
            return endPoints.Source.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }

        public override void Execute(IVertex vertex)
        {
            endPoints.Source = vertex;
            Source.VisualizeAsSource();
        }
    }
}
