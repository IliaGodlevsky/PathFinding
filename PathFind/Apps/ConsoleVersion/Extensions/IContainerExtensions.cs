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
            ILog log = null;
            try
            {
                log = container.Resolve<ILog>();
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
