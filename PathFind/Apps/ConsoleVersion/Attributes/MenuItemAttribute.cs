using ConsoleVersion.Enums;
using System;

namespace ConsoleVersion.Attributes
{
    /// <summary>
    /// Indicates that a method can be used to create a menu item
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MenuItemAttribute : Attribute
    {
        /// <summary>
        /// The relative position of the menu item in the menu list
        /// </summary>
        public MenuItemPriority Priority { get; }

        /// <summary>
        /// The header of a menu item that will be shown in the menu
        /// </summary>
        public string Header { get; }

        public MenuItemAttribute(string header, MenuItemPriority priority)
        {
            Header = header;
            Priority = priority;
        }
    }
}