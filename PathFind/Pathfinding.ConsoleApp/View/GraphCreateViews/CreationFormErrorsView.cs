using Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class CreationFormErrorsView : Terminal.Gui.View
    {
        private readonly CreationFormErrorsViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public CreationFormErrorsView(CreationFormErrorsViewModel viewModel) 
        { 
            this.viewModel = viewModel;
            Initialize();
            viewModel.WhenAnyValue(x => x.Message)
                .BindTo(this, x => x.errors.Text)
                .DisposeWith(disposables);
        }
    }
}
