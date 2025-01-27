using Pathfinding.App.Console.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed class GraphCopyView : Button
    {
        private readonly IGraphCopyViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public GraphCopyView(IGraphCopyViewModel viewModel)
        {
            X = Pos.Percent(33.34f);
            Y = 0;
            Width = Dim.Percent(16.67f);
            Text = "Copy";
            this.viewModel = viewModel;
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => Unit.Default)
                .InvokeCommand(viewModel, x => x.CopyGraphCommand)
                .DisposeWith(disposables);
        }
    }
}
