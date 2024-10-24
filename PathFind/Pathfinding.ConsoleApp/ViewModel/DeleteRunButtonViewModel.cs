using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class DeleteRunButtonViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog logger;

        private int[] runsIds = Array.Empty<int>();
        public int[] RunsIds
        {
            get => runsIds;
            set => this.RaiseAndSetIfChanged(ref runsIds, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> DeleteRunCommand { get; }

        public DeleteRunButtonViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            messenger.Register<RunSelectedMessage>(this, OnRunsSelected);
            DeleteRunCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(DeleteRuns, CanDelete());
        }

        private IObservable<bool> CanDelete()
        {
            return this.WhenAnyValue(x => x.RunsIds,
                ids => ids.Length > 0);
        }

        private async Task DeleteRuns(MouseEventArgs e)
        {
            await ExecuteSafe(async () =>
            {
                var isDeleted = await Task.Run(() => service.DeleteRunsAsync(RunsIds))
                    .ConfigureAwait(false);
                if (isDeleted)
                {
                    messenger.Send(new RunsDeletedMessage(RunsIds.ToArray()));
                }
            }, logger.Error).ConfigureAwait(false);
        }

        private void OnRunsSelected(object recipient, RunSelectedMessage msg)
        {
            RunsIds = msg.SelectedRuns;
        }
    }
}
