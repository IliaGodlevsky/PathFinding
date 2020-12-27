using ConsoleVersion.Enums;
using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal class MenuAttribute : Attribute
    {
        public MenuAttribute(string description, 
            MenuItemPriority menuItemPriority = MenuItemPriority.Normal)
        {
            Description = description;
            MenuItemPriority = menuItemPriority;
        }

        public MenuItemPriority MenuItemPriority { get; private set; }

        public string Description { get; private set; }
    }
}
