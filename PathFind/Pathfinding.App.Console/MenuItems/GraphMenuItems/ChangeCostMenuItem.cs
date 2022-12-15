using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model.VertexActions;
using System;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class ChangeCostMenuItem : SwitchVerticesMenuItem
    {
        public override int Order => 9;

        public ChangeCostMenuItem(IMessenger messenger, IInput<ConsoleKey> keyInput)
            : base(messenger, keyInput)
        {
            Actions.Add(ConsoleKey.UpArrow, new IncreaseCostAction());
            Actions.Add(ConsoleKey.DownArrow, new DecreaseCostAction());
        }

        public override string ToString()
        {
            return Languages.ChangeCost;
        }
    }
}
