using GraphLib.Extensions;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class IntermediateToReplaceCommands : BaseVerticesCommands
    {
        private const int CancelMarkToReplace = 0;

        public IntermediateToReplaceCommands(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        public override void Undo()
        {
            var marked = MarkedToReplace.ToArray();
            Commands[CancelMarkToReplace].ExecuteForEach(marked);
        }
    }
}