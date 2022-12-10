using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal sealed class EnterRequiredPathfindingRangeMenuItem : RangeMenuItem
    {
        private const int RequiredVerticesForRange = 2;

        public override int Order => 1;

        public EnterRequiredPathfindingRangeMenuItem(IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IInput<int> input, IMessenger messenger) 
            : base(rangeBuilder, messenger, input)
        {

        }

        public override bool CanBeExecuted()
        {
            return rangeBuilder.Range.Source == null
                && rangeBuilder.Range.Target == null
                && HasAvailableVerticesToIncludeInRange();
        }

        public override void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                System.Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
                InputVertices(RequiredVerticesForRange).ForEach(IncludeInRange);
            }
        }

        private bool HasAvailableVerticesToIncludeInRange()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        public override string ToString()
        {
            return "Enter pathfinding range";
        }
    }
}
