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
        protected readonly IMessenger messenger;
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
                .Select(GetName)
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
                var color = allColors[index];
                using (Cursor.HideCursor())
                {
                    SendColorsMessage(color);
                }
            }
        }

        protected virtual void SendColorsMessage(ConsoleColor color)
        {
            messenger.SendData(color, Token);
        }

        private static string GetName(ConsoleColor color)
        {
            return color switch
            {
                ConsoleColor.Black => Languages.Black,
                ConsoleColor.White => Languages.White,
                ConsoleColor.Red => Languages.Red,
                ConsoleColor.Green => Languages.Green,
                ConsoleColor.Blue => Languages.Blue,
                ConsoleColor.Yellow => Languages.Yellow,
                ConsoleColor.DarkGreen => Languages.DarkGreen,
                ConsoleColor.DarkGray => Languages.DarkGray,
                ConsoleColor.Gray => Languages.Gray,
                ConsoleColor.DarkCyan => Languages.DarkCyan,
                ConsoleColor.DarkRed => Languages.DarkRed,
                ConsoleColor.Cyan => Languages.Cyan,
                ConsoleColor.Magenta => Languages.Magenta,
                ConsoleColor.DarkMagenta => Languages.DarkMagenta,
                ConsoleColor.DarkBlue => Languages.DarkBlue,
                ConsoleColor.DarkYellow => Languages.DarkYellow,
                _ => throw new ArgumentOutOfRangeException(nameof(color))
            };
        }
    }
}