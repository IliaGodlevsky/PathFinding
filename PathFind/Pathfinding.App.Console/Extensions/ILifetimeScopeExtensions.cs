using Autofac;
using Pathfinding.App.Console.Views;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ILifetimeScopeExtensions
    {
        public static void Display<TView>(this ILifetimeScope lifetimeScope)
            where TView : View
        {
            using (var scope = lifetimeScope.BeginLifetimeScope())
            {
                var view = scope.Resolve<TView>();
                view.Display();
            }
        }
    }
}