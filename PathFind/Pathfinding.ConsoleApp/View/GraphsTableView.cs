using Pathfinding.ConsoleApp.ViewModel;
using System.Linq;
using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using Pathfinding.ConsoleApp.Model;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphsTableView
    {
        private readonly GraphTableViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public GraphsTableView(GraphTableViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.Events()
                .CellActivated
                .DistinctUntilChanged(x => x.Row)
                .Select(x => new GraphParametresModel()
                {
                    Width = (int)table.Rows[x.Row]["Width"],
                    Length = (int)table.Rows[x.Row]["Length"],
                    Name = (string)table.Rows[x.Row]["Name"],
                    Id = (int)table.Rows[x.Row]["Id"],
                    Obstacles = (int)table.Rows[x.Row]["Obstacles"]
                })
                .BindTo(viewModel, x => x.Selected)
                .DisposeWith(disposables);
            viewModel.Graphs.ActOnEveryObject(AddToTable, RemoveFromTable);
        }

        private void AddToTable(GraphParametresModel x)
        {
            table.Rows.Add(x.Id, x.Name, x.Width, x.Length, x.Obstacles);
            table.AcceptChanges();
        }

        private void RemoveFromTable(GraphParametresModel model)
        {
            var row = table.Rows.Find(model.Id);
            row.Delete();
            table.AcceptChanges();
        }
    }
}
