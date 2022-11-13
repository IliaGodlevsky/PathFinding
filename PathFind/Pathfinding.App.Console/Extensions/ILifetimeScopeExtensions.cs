using Autofac;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ILifetimeScopeExtensions
    {
        public static void Display<TView>(this ILifetimeScope lifetimeScope)
            where TView : IView
        {
            using (var scope = lifetimeScope.BeginLifetimeScope())
            {
                var view = scope.Resolve<TView>();
                view.Display();
            }
        }
    }
}