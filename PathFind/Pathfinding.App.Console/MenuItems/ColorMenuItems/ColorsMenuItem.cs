using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    internal abstract class ColorsMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> intInput;
        private readonly IReadOnlyList<ConsoleColor> allColors;
        private readonly MenuList allColorsMenuList;

        protected abstract Tokens Token { get; }

        protected ColorsMenuItem(IMessenger messenger, IInput<int> intInput)
        {
            this.messenger = messenger;
            this.intInput = intInput;
            allColors = Enum.GetValues(typeof(ConsoleColor))
                .Cast<ConsoleColor>()
                .ToArray();
            allColorsMenuList = allColors
                .Select(color => color.ToString())
                .Select(Languages.ResourceManager.GetString)
                .Append(Languages.Quit)
                .CreateMenuList(columnsNumber: 4);
        }

        public void Execute()
        {
            int index = default;
            using (Cursor.UseCurrentPositionWithClean())
            {
                string menu = allColorsMenuList + "\n" + Languages.ChooseColor;
                index = intInput.Input(menu, allColors.Count + 1, 1) - 1;
            }
            if (index != allColors.Count)
            {
                using (Cursor.HideCursor())
                {
                    messenger.SendData(allColors[index], Token);
                }
            }
        }
    }
}