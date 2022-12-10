using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal sealed class ReplaceTargetVertexMenuItem : ReplaceVerticesMenuItem
    {
        public override int Order => 3;

        protected override string Message => MessagesTexts.TargetVertexChoiceMsg;

        protected override Vertex Vertex => rangeBuilder.Range.Target;

        public ReplaceTargetVertexMenuItem(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            IMessenger messenger, IInput<int> input) : base(rangeBuilder, messenger, input)
        {

        }

        public override bool CanBeExecuted()
        {
            return rangeBuilder.Range.Target != null
                || rangeBuilder.Range.Target.IsIsolated();
        }

        public override string ToString()
        {
            return "Replace target vertex";
        }
    }
}
