using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;

namespace Pathfinding.App.Console.MenuItems
{
    [LowestPriority]
    internal class ExitMenuItem : IMenuItem
    {
        public virtual void Execute()
        {
            throw new ExitRequiredException();
        }

        public override string ToString()
        {
            return Languages.Exit;
        }
    }
}
