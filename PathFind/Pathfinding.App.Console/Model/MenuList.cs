using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.Model
{
    internal sealed class MenuList : IDisplayable
    {
        private const string NewLine = "\n";
        private const string Space = " ";
        private const string MenuFormat = "{0}. {1}";

        private readonly int menuItemsCount;
        private readonly int columns;
        private readonly int menuItemNumberPad;
        private readonly string[] menuItemsNames;

        private readonly Lazy<string> menuList;
        private readonly Lazy<int> longestNameLength;

        public MenuList(IEnumerable<string> menuItemsNames, int columns = 2)
        {
            this.menuItemsNames = menuItemsNames.ToArray();
            menuItemsCount = this.menuItemsNames.Length;
            menuItemNumberPad = menuItemsCount.ToString().Length;
            var columnsRange = new InclusiveValueRange<int>(menuItemsCount, 1);
            this.columns = columnsRange.ReturnInRange(columns);
            longestNameLength = new Lazy<int>(GetLongestNameLength);
            menuList = new Lazy<string>(CreateMenu);
        }

        public void Display()
        {
            ColorfulConsole.WriteLine(ToString());
        }

        public override string ToString()
        {
            return menuList.Value;
        }

        private int GetLongestNameLength()
        {
            return menuItemsCount > 0
                ? menuItemsNames.Max(str => str.Length) + 1
                : default;
        }

        private string CreateMenu()
        {
            var stringBuilder = new StringBuilder(NewLine);

            for (int index = 0; index < menuItemsCount; index++)
            {
                string paddedName = menuItemsNames[index].PadRight(longestNameLength.Value);
                string paddedMenuItemIndex = (index + 1).ToString().PadLeft(menuItemNumberPad);
                string format = MenuFormat + ((index + 1) % columns == 0 ? NewLine : Space);
                stringBuilder.AppendFormat(format, paddedMenuItemIndex, paddedName);
            }

            return stringBuilder.ToString();
        }
    }
}