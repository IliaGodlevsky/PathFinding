using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    internal abstract class ColorsMenuItem : IMenuItem
    {
        private readonly IInput<int> intInput;
        private readonly IReadOnlyList<ConsoleColor> allColors;
        private readonly MenuList allColorsMenuList;

        protected abstract string SettingKey { get; }

        protected ColorsMenuItem(IInput<int> intInput)
        {
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
                Colours.Default[SettingKey] = allColors[index];
            }
        }

        public override sealed string ToString()
        {
            return SettingKey;
        }
    }
}