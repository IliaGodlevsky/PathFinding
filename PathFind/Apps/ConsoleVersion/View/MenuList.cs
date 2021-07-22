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
            this.menuItemsNames = menuItemsNames;
            menuItemsCount = this.menuItemsNames.Count();
            columnsValueRange = new InclusiveValueRange<int>(menuItemsCount, 1);
            this.columns = columnsValueRange.ReturnInRange(columns);
            menuList = new Lazy<string>(CreateMenu);
        }

        public override string ToString()
        {
            return menuList.Value;
        }

        private string CreateMenu()
        {
            var menu = new StringBuilder(NewLine);

            int menuItemNumber = 0;
            int menuItemNumberPad = CalculateMenuItemNumberPad();
            int longestNameLength = menuItemsCount > 0
                ? menuItemsNames.Max(str => str.Length) + 1 : 0;

            foreach (var name in menuItemsNames)
            {
                string separator = CreateSeparator(menuItemNumber);
                string paddedName = name.PadRight(longestNameLength);
                string stringedMenuItemNumber = (++menuItemNumber).ToString();
                string paddedMenuItemNumber = stringedMenuItemNumber.PadLeft(menuItemNumberPad);
                string format = Format + separator;
                menu.AppendFormat(format, paddedMenuItemNumber, paddedName);
            }

            return menu.ToString();
        }

        private string CreateSeparator(int currentMenuItemNumber)
        {
            return (currentMenuItemNumber + 1) % columns == 0 ? NewLine : Space;
        }

        private int CalculateMenuItemNumberPad()
        {
            return menuItemsCount.ToString().Length;
        }

        private readonly IEnumerable<string> menuItemsNames;
        private readonly InclusiveValueRange<int> columnsValueRange;
        private readonly Lazy<string> menuList;
        private readonly int menuItemsCount;
        private readonly int columns;

        private const string NewLine = "\n";
        private const string Space = " ";
        private const string Format = "{0}. {1}";
    }
}
