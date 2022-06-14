using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(0)]
    internal sealed class RestoreSourceColorCommand : BaseEndPointsCommand
    {
        public RestoreSourceColorCommand(BaseEndPoints endPoints)
            : base(endPoints)
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