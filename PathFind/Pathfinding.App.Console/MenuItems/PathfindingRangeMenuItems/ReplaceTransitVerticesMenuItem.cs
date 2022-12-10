using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal sealed class ReplaceTransitVerticesMenuItem : RangeMenuItem
    {
        private const int RequiredSizeOfRange = 2;

        private const string NumberMessage = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
        private const string ReplaceMessage = MessagesTexts.IntermediateVertexChoiceMsg;
        private const string InputMessage = MessagesTexts.IntermediateVertexChoiceMsg;

        private readonly ReplaceTransitVerticesModule<Vertex> module;

        public override int Order => 5;

        public ReplaceTransitVerticesMenuItem(ReplaceTransitVerticesModule<Vertex>  module, 
            IPathfindingRangeBuilder<Vertex> rangeBuilder, IMessenger messenger, IInput<int> input) 
            : base(rangeBuilder, messenger, input)
        {
            this.module = module;
        }

        public override bool CanBeExecuted() => rangeBuilder.Range.Count() - RequiredSizeOfRange > 0;

        public override void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                int numberOfTransit = rangeBuilder.Range.Count() - RequiredSizeOfRange;
                int toReplaceNumber = input.Input(NumberMessage, numberOfTransit);
                System.Console.WriteLine(ReplaceMessage);
                input.InputExistingIntermediates(graph, rangeBuilder.Range, toReplaceNumber)
                    .ForEach(MarkTransitVertex);
                System.Console.WriteLine(InputMessage);
                InputVertices(toReplaceNumber).ForEach(ReplaceTransitVertex);
            }
        }

        private void MarkTransitVertex(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                module.MarkTransitVertex(rangeBuilder.Range, vertex);
            }
        }

        private void ReplaceTransitVertex(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                module.ReplaceTransitWith(rangeBuilder.Range, vertex);
            }
        }

        public override string ToString()
        {
            return "Replace transit vertices";
        }
    }
}
