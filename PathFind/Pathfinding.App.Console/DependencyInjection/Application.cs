using Autofac;
using Pathfinding.App.Console.MenuItems;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    internal sealed partial class Application : IDisposable
    {
        private readonly ContainerBuilder builder;
        private readonly Lazy<IContainer> scope;

        private IContainer Scope => scope.Value;

        public Application(string title = Constants.Title)
        {
            Terminal.Title = title;
            builder = new ContainerBuilder();
            scope = new(() => BuildApplication(builder));
        }

        public void ApplyComponents()
        {
            Parallel.ForEach(Components, x => x.Apply(builder));
        }

        public void Run(Encoding outputEncoding)
        {
            Terminal.OutputEncoding = outputEncoding;
            Scope.Resolve<MainUnitMenuItem>().Execute();
        }

        public void Run() => Run(Encoding.UTF8);

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}