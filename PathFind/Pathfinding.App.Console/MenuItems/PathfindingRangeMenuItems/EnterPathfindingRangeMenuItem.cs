using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal sealed class EnterPathfindingRangeMenuItem : SwitchVerticesMenuItem
    {
        public override int Order => 1;

        public EnterPathfindingRangeMenuItem(IMessenger messenger, 
            IPathfindingRangeBuilder<Vertex> rangeBuilder, IInput<ConsoleKey> keyInput) 
            : base(messenger, keyInput)
        {
            Actions.Add(ConsoleKey.Enter, new IncludeInRangeAction(rangeBuilder));
            Actions.Add(ConsoleKey.X, new ExcludeFromRangeAction(rangeBuilder));
        }

        public override bool CanBeExecuted()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        public override string ToString()
        {
            return "Enter pathfinding range";
        }
    }
}
