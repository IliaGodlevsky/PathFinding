using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class ReverseVertexMenuItem : SwitchVerticesMenuItem
    {
        public override int Order => 10;

        public ReverseVertexMenuItem(IMessenger messenger, IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            IInput<ConsoleKey> keyInput) : base(messenger, keyInput)
        {
            actions.Add(ConsoleKey.Enter, new ReverseVertexAction(rangeBuilder.Range, messenger));
        }

        public override string ToString()
        {
            return Languages.ReverseVertices;
        }
    }
}
