using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class AlgorithmsListView : FrameView
    {
        public AlgorithmsListView([KeyFilter(KeyFilters.AlgorithmsListView)]IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            var names = children.Select(x => x.Text).ToList();
            algorithms.SetSource(names);
            algorithms.Add(children.ToArray());
        }
    }
}
