using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using System.Reactive;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using System;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class AlgorithmUpdateView : Button
    {
        private readonly CompositeDisposable disposables = new();

        public AlgorithmUpdateView(IAlgorithmUpdateViewModel viewModel) 
        {
            X = Pos.Percent(33);
            Y = 0;
            Width = Dim.Percent(33);
            Text = "Update";
            this.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Throttle(TimeSpan.FromMilliseconds(30))
                .Select(x => Unit.Default)
                .InvokeCommand(viewModel, x => x.UpdateAlgorithmsCommand)
                .DisposeWith(disposables);
        }
    }
}
