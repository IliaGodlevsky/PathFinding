using GraphLib.Extensions;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class SetEndPointsCommands : BaseVerticesCommands
    {
        private const int UnsetSource = 0;
        private const int UnsetTarget = 1;
        private const int UnsetIntermedate = 2;

        public SetEndPointsCommands(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        public override void Reset()
        {
            Commands[UnsetSource].Execute(endPoints.Source);
            Commands[UnsetTarget].Execute(endPoints.Target);
            Commands[UnsetIntermedate].ExecuteForEach(Intermediates.ToArray());
        }
    }
}