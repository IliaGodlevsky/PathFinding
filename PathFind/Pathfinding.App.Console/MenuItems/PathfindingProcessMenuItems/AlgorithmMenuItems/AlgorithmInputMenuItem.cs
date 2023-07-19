using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    internal abstract class AlgorithmInputMenuItem : AlgorithmMenuItem
    {
        protected readonly IInput<int> intInput;

        protected AlgorithmInputMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger)
        {
            this.intInput = intInput;
        }

        protected (TKey Key, TValue Value) InputItem<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> items, string inputMessage)
        {
            if (items.Count == 0)
            {
                throw new Exception(Languages.NoItemsMsg);
            }

            if (items.Count == 1)
            {
                var item = items.First();
                return (item.Key, item.Value);
            }

            string menu = items.Keys.Select(GetString)
                .CreateMenuList(1)
                .ToString();

            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = intInput.Input(menu + inputMessage, items.Count, 1) - 1;
                var item = items.ElementAt(index);
                return (item.Key, item.Value);
            }
        }

        private static string GetString<T>(T key)
        {
            string name = key.ToString();
            return Languages.ResourceManager.GetString(name) ?? name;
        }
    }
}
