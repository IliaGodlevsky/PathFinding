using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Data.Extensions;
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
    internal sealed class GraphCopyViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog log;

        public ReactiveCommand<MouseEventArgs, Unit> CopyGraphCommand { get; }

        private int[] selectedGraphIds = Array.Empty<int>();
        public int[] SelectedGraphIds
        {
            get => selectedGraphIds;
            set => this.RaiseAndSetIfChanged(ref selectedGraphIds, value);
        }

        public GraphCopyViewModel(
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger, 
            IRequestService<GraphVertexModel> service,
            ILog log)
        {
            this.messenger = messenger;
            this.service = service;
            this.log = log;
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            CopyGraphCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(ExecuteCopy, CanExecute());
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            SelectedGraphIds = msg.Graphs.Select(x => x.Id).ToArray();
        }

        private async Task ExecuteCopy(MouseEventArgs e)
        {
            await ExecuteSafe(async () =>
            {
                var copies = await service.ReadSerializationHistoriesAsync(SelectedGraphIds)
                    .ConfigureAwait(false);
                var histories = await service.CreatePathfindingHistoriesAsync(copies)
                    .ConfigureAwait(false);
                var graphs = histories.Select(x => new GraphInfoModel()
                {
                    Width = x.Graph.Graph.GetWidth(),
                    Length = x.Graph.Graph.GetLength(),
                    Name = x.Graph.Name,
                    Neighborhood = x.Graph.Neighborhood,
                    Id = x.Graph.Id,
                    SmoothLevel = x.Graph.SmoothLevel,
                    Obstacles = x.Graph.Graph.GetObstaclesCount(),
                    Status = x.Graph.IsReadOnly ? GraphStatuses.Readonly : GraphStatuses.Editable
                }).ToArray();
                messenger.Send(new GraphCreatedMessage(graphs));
            }, log.Error).ConfigureAwait(false);
        }

        private IObservable<bool> CanExecute()
        {
            return this.WhenAnyValue(x => x.SelectedGraphIds,
                (ids) => ids.Length > 0);
        }
    }
}
