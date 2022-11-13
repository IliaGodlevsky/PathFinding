using System;

namespace Pathfinding.App.Console.Model.MenuCommands.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class MenuColumnsNumberAttribute : Attribute
    {
        public static MenuColumnsNumberAttribute Default
            = new MenuColumnsNumberAttribute(2);

        public int MenuColumns { get; }

        public MenuColumnsNumberAttribute(int menuColumns)
        {
            MenuColumns = menuColumns;
        }
    }
}
