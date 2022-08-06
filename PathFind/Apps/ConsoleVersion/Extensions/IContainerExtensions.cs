using Autofac;
using ConsoleVersion.Interface;

namespace ConsoleVersion.Extensions
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