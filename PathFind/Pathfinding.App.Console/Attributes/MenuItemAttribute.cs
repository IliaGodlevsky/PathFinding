using System;
using System.ComponentModel;

namespace Pathfinding.App.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MenuItemAttribute : DescriptionAttribute
    {
        public MenuItemAttribute(string header) 
            : base(header)
        {
            
        }
    }
}