using Common.ValueRanges;
using ConsoleVersion.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleVersion.View
{
    internal sealed class MenuList
    {
        public MenuList(ICollection<string> menuItemsNames, int columns = 2)
        {
            columnsValueRange = new InclusiveValueRange<int>(10, 1);
            this.columns = columnsValueRange.ReturnInRange(columns);
            this.menuItemsNames = menuItemsNames;
            menuList = new Lazy<string>(CreateMenu);
        }

        public override string ToString()
        {
            return menuList.Value;
        }

        private string CreateMenu()
        {
            var menu = new StringBuilder("\n");

            int menuItemNumber = 0;
            int menuItemNumberPad = Convert.ToInt32(Math.Log10(menuItemsNames.Count)) + 1;
            int longestNameLength = menuItemsNames.Max(str => str.Length) + 1;

            foreach (var name in menuItemsNames)
            {
                string separator = CreateSeparator(menuItemNumber);
                string paddedName = name.PadRight(longestNameLength);
                string stringedMenuItemNumber = (++menuItemNumber).ToString();
                string paddedMenuItemNumber = stringedMenuItemNumber.PadLeft(menuItemNumberPad);
                string format = Resources.MenuFormat + separator;
                menu.AppendFormat(format, paddedMenuItemNumber, paddedName);
            }

            return menu.ToString();
        }

        private string CreateSeparator(int currentMenuItemNumber)
        {
            return (currentMenuItemNumber + 1) % columns == 0 ? "\n" : " ";
        }

        private readonly Lazy<string> menuList;

        private readonly int columns;
        private readonly ICollection<string> menuItemsNames;
        private readonly IValueRange<int> columnsValueRange;
    }
}
