using GraphLib.Base.EndPoints.Extensions;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class IntermediateToReplaceCommands : BaseVerticesCommands
    {
        private const int CancelMarkToReplace = 0;

        public IntermediateToReplaceCommands(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        public override void Reset()
        {
            var marked = endPoints.markedToReplaceIntermediates.ToArray();
            Commands[CancelMarkToReplace].ExecuteForEach(marked);
        }
    }
}