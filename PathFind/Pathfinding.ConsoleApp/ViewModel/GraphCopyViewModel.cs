using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Extensions;
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
    internal sealed class GraphCopyViewModel : BaseViewModel, IGraphCopyViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog log;

        public ReactiveCommand<Unit, Unit> CopyGraphCommand { get; }

        private int[] graphIds = Array.Empty<int>();
        private int[] GraphIds
        {
            get => graphIds;
            set => this.RaiseAndSetIfChanged(ref graphIds, value);
        }

        public GraphCopyViewModel(
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog log)
        {
            this.messenger = messenger;
            this.service = service;
            this.log = log;
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            CopyGraphCommand = ReactiveCommand.CreateFromTask(ExecuteCopy, CanExecute());
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            GraphIds = msg.Graphs.Select(x => x.Id).ToArray();
        }

        private async Task ExecuteCopy()
        {
            await ExecuteSafe(async () =>
            {
                var copies = await service.ReadSerializationHistoriesAsync(GraphIds)
                    .ConfigureAwait(false);
                var histories = await service.CreatePathfindingHistoriesAsync(copies.Histories)
                    .ConfigureAwait(false);
                var graphs = histories.Select(x => x.Graph).ToGraphInfo();
                messenger.Send(new GraphCreatedMessage(graphs));
            }, log.Error).ConfigureAwait(false);
        }

        private IObservable<bool> CanExecute()
        {
            return this.WhenAnyValue(x => x.GraphIds,
                (ids) => ids.Length > 0);
        }
    }
}
