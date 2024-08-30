using Pathfinding.App.Console.ViewModel;
using System.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using Pathfinding.Service.Interface.Models.Read;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Pathfinding.App.Console.View
{
    public class GraphsView : Terminal.Gui.View
    {
        private readonly GraphsViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public GraphsView(GraphsViewModel viewModel) 
        {
            this.viewModel = viewModel;
            var viewPort = new FrameView("Graphs");

            var table = new ListView(viewModel.Graphs)
            {
                X = 1,
                Y = 1,
                Width = Dim.Percent(20.0f),
                Height = Dim.Percent(50.0f),
            };
            table
                .Events()
                .SelectedItemChanged
                .Select(x => (GraphInformationModel)x.Value)
                .BindTo(viewModel, x => x.CurrentGraph)
                .DisposeWith(disposables);
            var newButton = new Button("New")
            {

            };
            this.viewModel = viewModel;
            Add(table, newButton);
        }
    }
}
