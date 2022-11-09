using Autofac;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IContainerExtensions
    {
        public static void Display<TView>(this IContainer container)
            where TView : IView
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var view = scope.Resolve<TView>();
                view.Display();
            }
        }
    }
}