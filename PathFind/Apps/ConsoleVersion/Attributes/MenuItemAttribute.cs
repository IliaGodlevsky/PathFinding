using ConsoleVersion.Enums;
using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MenuItemAttribute : Attribute
    {
        public MenuItemPriority Priority { get; }

        public string Header { get; }

        public MenuItemAttribute(string header, MenuItemPriority priority)
        {
            Header = header;
            Priority = priority;
        }
    }
}