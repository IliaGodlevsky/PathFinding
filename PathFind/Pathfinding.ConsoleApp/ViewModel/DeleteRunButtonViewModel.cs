using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class DeleteRunButtonViewModel : BaseViewModel, IDeleteRunViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog logger;

        private int[] runsIds = Array.Empty<int>();
        private int[] RunsIds
        {
            get => runsIds;
            set => this.RaiseAndSetIfChanged(ref runsIds, value);
        }

        public ReactiveCommand<Unit, Unit> DeleteRunCommand { get; }

        public DeleteRunButtonViewModel(
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            messenger.Register<RunSelectedMessage>(this, OnRunsSelected);
            DeleteRunCommand = ReactiveCommand.CreateFromTask(DeleteRuns, CanDelete());
        }

        private IObservable<bool> CanDelete()
        {
            return this.WhenAnyValue(x => x.RunsIds,
                ids => ids.Length > 0);
        }

        private async Task DeleteRuns()
        {
            await ExecuteSafe(async () =>
            {
                var isDeleted = await service.DeleteRunsAsync(RunsIds)
                    .ConfigureAwait(false);
                if (isDeleted)
                {
                    var runs = RunsIds.ToArray();
                    RunsIds = Array.Empty<int>();
                    messenger.Send(new RunsDeletedMessage(runs));
                }
            }, logger.Error).ConfigureAwait(false);
        }

        private void OnRunsSelected(object recipient, RunSelectedMessage msg)
        {
            RunsIds = msg.SelectedRuns.Select(x => x.Id).ToArray();
        }
    }
}
