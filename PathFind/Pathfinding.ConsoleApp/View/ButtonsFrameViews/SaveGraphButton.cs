using Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class SaveGraphButton
    {
        private readonly IMessenger messenger;
        private readonly SaveGraphButtonModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SaveGraphButton([KeyFilter(KeyFilters.Views)]IMessenger messenger, 
            SaveGraphButtonModel viewModel)
        {
            this.messenger = messenger;
            this.viewModel = viewModel;
            Initialize();
            this.Events().MouseClick
                .Where(e => e.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Do(async x => await OnSaveButtonClicked(x))
                .InvokeCommand(viewModel, x => x.SaveGraphCommand)
                .DisposeWith(disposables);
        }

        private async Task OnSaveButtonClicked(MouseEventArgs e)
        {
            using (var dialog = new SaveDialog())
            {
                dialog.Width = Dim.Percent(45);
                dialog.Height = Dim.Percent(55);
                Application.Run(dialog);
                if (!dialog.Canceled && dialog.FileName != null)
                {
                    viewModel.FilePath = dialog.FilePath.ToString();
                }
            }
            await Task.CompletedTask;
        }
    }
}
