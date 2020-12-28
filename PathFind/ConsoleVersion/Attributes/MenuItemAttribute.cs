using ConsoleVersion.Enums;
using System;

namespace ConsoleVersion.Attributes
{
    /// <summary>
    /// Indicates that a method can be used to create a menu item
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal class MenuItemAttribute : Attribute
    {
        public MenuItemAttribute(string menuItemName, 
            MenuItemPriority menuItemPriority = MenuItemPriority.Normal)
        {
            MenuItemName = menuItemName;
            MenuItemPriority = menuItemPriority;
        }

        public MenuItemPriority MenuItemPriority { get; private set; }

        public string MenuItemName { get; private set; }
    }
}
