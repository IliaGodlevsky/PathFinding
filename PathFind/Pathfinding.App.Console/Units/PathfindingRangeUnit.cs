using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingRangeUnit : Unit, ICanRecieveMessage
    {
        public PathfindingRangeUnit(IReadOnlyCollection<IMenuItem> menuItems)
            : base(menuItems)
        {

        }

        public void RegisterHanlders(IMessenger messenger)
        {
            //throw new System.NotImplementedException();
        }

        //private void RecieveRangeVertex(VertexActionMessage msg)
        //{
        //    ConsoleKey key = msg.Key;
        //    var vertex = msg.Vertex;
        //}
    }
}