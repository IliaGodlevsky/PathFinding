using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems
{
    [LowestPriority]
    internal class ExitMenuItem : IMenuItem
    {
        public virtual async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            await Task.CompletedTask;
            throw new ExitRequiredException();
        }

        public override string ToString()
        {
            return Languages.Exit;
        }
    }
}
