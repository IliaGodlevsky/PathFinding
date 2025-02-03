using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class RunCreateButton : Button
    {
        public RunCreateButton(RunCreateView view,
            IGraphFieldViewModel viewModel)
        {
            Initialize();

            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(x => view.Visible = true)
                .Subscribe();
            viewModel.WhenAnyValue(x => x.Graph)
                .Select(x => x != Graph<GraphVertexModel>.Empty)
                .BindTo(this, x => x.Enabled);
        }
    }
}
