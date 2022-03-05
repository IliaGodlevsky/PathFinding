using Autofac;
using ConsoleVersion.Interface;
using Logging.Interface;
using System;

namespace ConsoleVersion.Extensions
{
    internal static class IContainerExtensions
    {
        public static void Display<TView>(this IContainer container)
            where TView : IView
        {
            var log = container.Resolve<ILog>();
            try
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var view = scope.Resolve<TView>();
                    view.Display();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
