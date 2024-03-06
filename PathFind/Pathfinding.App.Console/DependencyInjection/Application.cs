using Autofac;
using Pathfinding.App.Console.MenuItems;
using System;
using System.Text;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    internal sealed class Application : IDisposable
    {
        private readonly ContainerBuilder builder = new();
        private readonly Lazy<IContainer> scope;

        private IContainer Scope => scope.Value;

        public Application(string title = Constants.Title)
        {
            Terminal.Title = title;
            scope = new(Build);
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

        private IContainer Build()
        {
            return builder
                .AddDataAccessLayer()
                .AddAlgorithms()
                .AddTransitVertices()
                .AddColorsEditor()
                .AddGraphEditor()
                .AddKeysEditor()
                .AddGraphSharing()
                .AddPathfindingControl()
                .AddPathfindingHistory()
                .AddPathfindingStatistics()
                .AddPathfindingVisualization()
                .BuildApplication();
        }
    }
}