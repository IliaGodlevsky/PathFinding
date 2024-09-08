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
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class SaveGraphButton
    {
        private readonly IMessenger messenger;
        private readonly SaveGraphButtonViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public SaveGraphButton([KeyFilter(KeyFilters.Views)]IMessenger messenger, 
            SaveGraphButtonViewModel viewModel)
        {
            this.messenger = messenger;
            this.viewModel = viewModel;
            Initialize();
            var commandObservable = viewModel.SaveGraphCommand.CanExecute;
            var clickObservable = this.Events().MouseClick;
            clickObservable.CombineLatest(commandObservable)
                .Where(x => x.First.MouseEvent.Flags == MouseFlags.Button1Clicked
                    && x.Second == true)
                .Select(x => x.First)
                .Do(async x => await OnSaveButtonClicked())
                .InvokeCommand(viewModel, x => x.SaveGraphCommand)
                .DisposeWith(disposables);
        }

        private async Task OnSaveButtonClicked()
        {
            var allowedTypes = new List<string>() { ".json" };
            using (var dialog = new SaveDialog("Export", "", allowedTypes))
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
