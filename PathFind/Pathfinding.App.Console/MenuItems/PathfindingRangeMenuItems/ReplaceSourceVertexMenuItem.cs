using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal sealed class ReplaceSourceVertexMenuItem : ReplaceVerticesMenuItem
    {
        protected override string Message => MessagesTexts.SourceVertexChoiceMsg;

        protected override Vertex Vertex => rangeBuilder.Range.Source;

        public override int Order => 2;

        public ReplaceSourceVertexMenuItem(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            IMessenger messenger, IInput<int> input) : base(rangeBuilder, messenger, input)
        {

        }

        public override string ToString()
        {
            return "Replace source vertex";
        }

        public override bool CanBeExecuted()
        {
            return rangeBuilder.Range.Source != null
                || rangeBuilder.Range.Source.IsIsolated();
        }
    }
}