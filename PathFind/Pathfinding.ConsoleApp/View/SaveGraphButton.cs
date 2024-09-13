using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class SaveGraphButton
    {
        private readonly IMessenger messenger;
        private readonly SaveGraphButtonViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public SaveGraphButton([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            SaveGraphButtonViewModel viewModel)
        {
            this.messenger = messenger;
            this.viewModel = viewModel;
            Initialize();
            var commandObservable = viewModel.SaveGraphCommand.CanExecute;
            var clickObservable = this.Events().MouseClick;
            clickObservable.Zip(commandObservable)
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
