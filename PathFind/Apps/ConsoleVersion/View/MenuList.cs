using Common.Extensions;
using Common.ValueRanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleVersion.View
{
    internal sealed class MenuList
    {
        public MenuList(IEnumerable<string> menuItemsNames, int columns = 2)
        {
            this.menuItemsNames = menuItemsNames.ToArray();
            menuItemsCount = this.menuItemsNames.Length;
            MenuItemNumberPad = menuItemsCount.ToString().Length;
            var columnsValueRange = new InclusiveValueRange<int>(menuItemsCount, 1);
            this.columns = columnsValueRange.ReturnInRange(columns);
            LongestNameLength = menuItemsCount > 0 ? menuItemsNames.Max(str => str.Length) + 1 : 0;
            menuList = new Lazy<string>(CreateMenu);
        }

        public override string ToString()
        {
            return menuList.Value;
        }

        private string CreateMenu()
        {
            return new StringBuilder(NewLine)
                .AppendRepeat(GetFormattedMenuItem, menuItemsCount)
                .ToString();
        }

        private string GetFormattedMenuItem(int menuItemIndex)
        {
            string paddedName = menuItemsNames[menuItemIndex].PadRight(LongestNameLength);
            string paddedMenuItemIndex = (menuItemIndex + 1).ToString().PadLeft(MenuItemNumberPad);
            string format = Format + ((menuItemIndex + 1) % columns == 0 ? NewLine : Space);
            return string.Format(format, paddedMenuItemIndex, paddedName);
        }

        private int MenuItemNumberPad { get; }
        private int LongestNameLength { get; }

        private readonly string[] menuItemsNames;
        private readonly Lazy<string> menuList;
        
        private readonly int menuItemsCount;
        private readonly int columns;

        private const string NewLine = "\n";
        private const string Space = " ";
        private const string Format = "{0}. {1}";
    }
}