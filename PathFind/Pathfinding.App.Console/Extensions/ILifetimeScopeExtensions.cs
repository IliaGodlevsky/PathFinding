using Autofac;
using Pathfinding.App.Console.Views;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ILifetimeScopeExtensions
    {
        public static void Display<TView>(this ILifetimeScope lifetimeScope)
            where TView : View
        {
            var view = lifetimeScope.Resolve<TView>();
            view.Display();
        }

        public static void DisplayScoped<TView>(this ILifetimeScope lifetimeScope)
            where TView : View
        {
            using(var scope = lifetimeScope.BeginLifetimeScope())
            {
                scope.Display<TView>();
            }
        }
    }
}