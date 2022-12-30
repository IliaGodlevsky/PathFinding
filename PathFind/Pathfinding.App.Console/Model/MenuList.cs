using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            this.menuItemsCount = this.menuItemsNames.Length;
            this.menuItemNumberPad = menuItemsCount.GetDigitsNumber();
            var columnsRange = new InclusiveValueRange<int>(menuItemsCount, 1);
            this.columns = columnsRange.ReturnInRange(columns);
            this.longestNameLength = new(GetLongestNameLength);
            this.menuList = new(CreateMenu);
        }

        public void Display()
        {
            System.Console.WriteLine(ToString());
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
            var stringBuilder = new StringBuilder();

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