using Common.Extensions;
using Common.ValueRanges;
using ConsoleVersion.View.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleVersion.View
{
    internal sealed class MenuList : IDisplayable
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

        public void Display()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return menuList.Value;
        }

        private string CreateMenu()
        {
            var stringBuilder = new StringBuilder(NewLine);

            for (int i = 0; i < menuItemsCount; i++)
            {
                string paddedName = menuItemsNames[i].PadRight(LongestNameLength);
                string paddedMenuItemIndex = (i + 1).ToString().PadLeft(MenuItemNumberPad);
                string format = Format + ((i + 1) % columns == 0 ? NewLine : Space);
                stringBuilder.AppendFormat(format, paddedMenuItemIndex, paddedName);
            }

            return stringBuilder.ToString();
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