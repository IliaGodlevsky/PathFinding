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
        public MenuItemAttribute(string header,
            MenuItemPriority priority = MenuItemPriority.Normal)
        {
            Header = header;
            Priority = priority;
        }

        public MenuItemPriority Priority { get; }

        /// <summary>
        /// The header of a menu item that will be shown in the menu
        /// </summary>
        public string Header { get; }
    }
}
