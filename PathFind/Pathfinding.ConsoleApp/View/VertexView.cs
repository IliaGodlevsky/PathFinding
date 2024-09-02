using Pathfinding.ConsoleApp.ViewModel;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    public class VertexView : Label
    {
        private readonly CompositeDisposable disposables;

        public VertexView(VertexViewModel viewModel)
        {
            disposables = new CompositeDisposable();
            viewModel.WhenAnyValue(x => x.IsObstacle)
                .Select(x => x ? Create(Color.Black) : Create(Color.White))
                .BindTo(this, x => x.ColorScheme)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Color)
                .Select(Create)
                .BindTo(this, x => x.ColorScheme)
                .DisposeWith(disposables);
            viewModel.Color = viewModel.IsObstacle ? Color.Black : Color.White;
        }

        private ColorScheme Create(Color foreground)
        {
            return new() { Normal 
                = Application.Driver.MakeAttribute(foreground, Color.Black) };
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
