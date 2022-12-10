using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal sealed class EnterTransitVerticesMenuItem : RangeMenuItem
    {
        private const string NumberMessage = MessagesTexts.NumberOfTransitVerticesInputMsg;
        private const string InputMessage = MessagesTexts.IntermediateVertexChoiceMsg;

        public override int Order => 4;

        public EnterTransitVerticesMenuItem(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            IMessenger messenger, IInput<int> input) : base(rangeBuilder, messenger, input)
        {

        }

        public override bool CanBeExecuted()
        {
            return rangeBuilder.Range.Source != null
                && rangeBuilder.Range.Target != null
                && !rangeBuilder.Range.Transit.Any(vertex => vertex.IsObstacle);
        }

        public override void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                int available = graph.GetAvailableTransitVerticesNumber();
                int number = input.Input(NumberMessage, available);
                System.Console.WriteLine(InputMessage);
                InputVertices(number).ForEach(IncludeInRange);
            }
        }

        public override string ToString()
        {
            return "Enter transit vertices";
        }
    }
}
