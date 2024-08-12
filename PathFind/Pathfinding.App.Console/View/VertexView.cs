using Pathfinding.App.Console.ViewModel;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    public class VertexView : Label
    {
        private readonly CompositeDisposable disposables;

        public VertexView(VertexViewModel viewModel)
        {
            Data = viewModel;
            disposables = new();
            viewModel.Cost.WhenAnyValue(x => x.CurrentCost)
                .BindTo(this, x => x.Text)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Cost)
                .Select(x => x.CurrentCost.ToString())
                .BindTo(this, x => x.Text)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Color)
                .Select(x => new ColorScheme()
                {
                    Normal = Application.Driver.MakeAttribute(x, Color.Black)
                })
                .BindTo(this, x => x.ColorScheme)
                .DisposeWith(disposables);
        }
    }
}
