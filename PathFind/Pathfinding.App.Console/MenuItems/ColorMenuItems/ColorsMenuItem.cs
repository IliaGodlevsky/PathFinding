using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    internal abstract class ColorsMenuItem : IMenuItem
    {
        protected readonly IMessenger messenger;
        private readonly IInput<int> intInput;
        private readonly IReadOnlyList<ConsoleColor> allColors;
        private readonly IReadOnlyList<PropertyInfo> properties;
        private readonly MenuList allColorsMenuList;
        private readonly MenuList menuItemsColorsMenuList;

        protected ColorsMenuItem(IMessenger messenger, IInput<int> intInput)
        {
            this.messenger = messenger;
            this.intInput = intInput;
            var enumType = typeof(ConsoleColor);
            allColors = Enum.GetValues(enumType).Cast<ConsoleColor>().ToArray();
            allColorsMenuList = allColors.CreateMenuList(GetName);
            properties = GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(prop => prop.PropertyType == enumType)
                .ToArray();
            menuItemsColorsMenuList = properties
                .Select(prop => prop.GetAttributeOrDefault<DescriptionAttribute>().Description)
                .Append(Languages.Quit)
                .CreateMenuList();
        }

        public void Execute()
        {
            int index = GetIndex(menuItemsColorsMenuList, properties.Count + 1, 1);
            while (index != properties.Count)
            {
                int toChangeIndex = GetIndex(allColorsMenuList, allColors.Count, 1);
                var colorToChange = allColors[toChangeIndex];
                properties[index].SetValue(this, colorToChange);
                index = GetIndex(menuItemsColorsMenuList, properties.Count + 1, 1);
            }
            SendColorsMessage();
        }

        private int GetIndex(MenuList menuList, int limit, int bottom)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                menuList.Display();
                return intInput.Input(Languages.ChooseColor, limit, bottom) - 1;
            }
        }

        protected abstract void SendColorsMessage();

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