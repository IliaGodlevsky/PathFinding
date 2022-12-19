using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [Order(10)]
    internal sealed class ReverseVertexMenuItem : SwitchVerticesMenuItem
    {
        public ReverseVertexMenuItem(IMessenger messenger, 
            IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            IInput<ConsoleKey> keyInput) 
            : base(messenger, keyInput)
        {
            Actions.Add(ConsoleKey.Enter, new ReverseVertexAction(rangeBuilder.Range, messenger));
        }

        public override string ToString()
        {
            return Languages.ReverseVertices;
        }
    }
}
