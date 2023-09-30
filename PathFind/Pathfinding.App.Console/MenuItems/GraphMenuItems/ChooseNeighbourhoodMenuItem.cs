using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Factory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class ChooseNeighbourhoodMenuItem : IMenuItem
    {
        private readonly IReadOnlyDictionary<string, INeighborhoodFactory> factories;
        private readonly IMessenger messenger;
        private readonly IInput<int> intInput;

        public ChooseNeighbourhoodMenuItem(IMessenger messenger, 
            IInput<int> intInput,
            IReadOnlyDictionary<string, INeighborhoodFactory> factories)
        {
            this.messenger = messenger;
            this.intInput = intInput;
            this.factories = factories;
        }

        public void Execute()
        {
            if (factories.Count == 0)
            {
                throw new Exception(Languages.NoItemsMsg);
            }

            if (factories.Count == 1)
            {
                messenger.SendData(factories.ElementAt(0).Value, Tokens.Graph);
                return;
            }

            string menu = factories
                .Select(f => Languages.ResourceManager.GetString(f.Key.ToString()))
                .CreateMenuList(1)
                .ToString();
            int index = InputIndex(menu + Languages.ChooseNeighbourhoodMsg, factories.Count);
            var factory = factories.ElementAt(index).Value;
            messenger.SendData(factory, Tokens.Graph);
        }

        private int InputIndex(string menu, int limit)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = intInput.Input(menu, limit, 1) - 1;
                return index;
            }
        }

        public override string ToString()
        {
            return Languages.ChooseNeighbourhood;
        }
    }
}
