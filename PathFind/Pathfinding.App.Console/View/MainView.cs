using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.Interface;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.View
{
    internal class MainView : Terminal.Gui.View
    {
        private readonly IReadOnlyDictionary<string, IViewFactory<Terminal.Gui.View>> factories;

        public MainView([KeyFilter("MainView")]IEnumerable<IViewFactory<Terminal.Gui.View>> factories)
        {
            this.factories = factories.ToDictionary(x => x.GetType().Name).AsReadOnly();
            var graphView = this.factories.GetOrDefault(typeof(GraphView).Name);
            if (graphView != null)
            {
                Add(graphView.CreateView());
            }
        }
    }
}
