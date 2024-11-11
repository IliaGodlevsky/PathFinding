using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class GraphCopyView : Button
    {
        private readonly GraphCopyViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public GraphCopyView(GraphCopyViewModel viewModel)
        {
            X = Pos.Percent(33.34f);
            Y = 0;
            Width = Dim.Percent(16.67f);
            Text = "Copy";
            this.viewModel = viewModel;
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .InvokeCommand(viewModel, x => x.CopyGraphCommand)
                .DisposeWith(disposables);
        }
    }
}
