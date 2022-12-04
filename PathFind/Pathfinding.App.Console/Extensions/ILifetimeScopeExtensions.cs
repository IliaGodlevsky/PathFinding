using Autofac;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Views;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ILifetimeScopeExtensions
    {
        public static void Display<TViewModel>(this ILifetimeScope lifetimeScope)
            where TViewModel : IViewModel
        {
            var view = lifetimeScope.Resolve<View<TViewModel>>();
            view.Display();
        }
    }
}