using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(RestoreColorsCommands)), Order(0)]
    internal sealed class RestoreSourceColorCommand : BaseEndPointsCommand
    {
        public RestoreSourceColorCommand(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsSource();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return vertex.Equals(endPoints.Source);
        }
    }
}
