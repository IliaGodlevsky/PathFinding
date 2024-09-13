using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class PathfindingRangeViewModel : BaseViewModel, IPathfindingRange<VertexModel>
    {
        private readonly CompositeDisposable disposables = new();
        private readonly IRequestService<VertexModel> service;
        private readonly IEnumerable<IPathfindingRangeCommand<VertexModel>> includeCommands;
        private readonly IEnumerable<IPathfindingRangeCommand<VertexModel>> excludeCommands;
        private readonly IPathfindingRange<VertexModel> pathfindingRange;
        private readonly ILog logger;

        private int GraphId { get; set; }

        private IGraph<VertexModel> Graph { get; set; }

        private VertexModel source;
        public VertexModel Source
        {
            get => source;
            set => this.RaiseAndSetIfChanged(ref source, value);
        }

        private VertexModel target;
        public VertexModel Target
        {
            get => target;
            set => this.RaiseAndSetIfChanged(ref target, value);
        }

        public ReactiveCommand<VertexModel, Unit> AddToRangeCommand { get; }

        public ReactiveCommand<VertexModel, Unit> RemoveFromRangeCommand { get; }

        public ObservableCollection<VertexModel> Transit { get; private set; } = new();

        IList<VertexModel> IPathfindingRange<VertexModel>.Transit => Transit;

        public PathfindingRangeViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<VertexModel> service,
            [KeyFilter(KeyFilters.IncludeCommands)] IEnumerable<IPathfindingRangeCommand<VertexModel>> includeCommands,
            [KeyFilter(KeyFilters.ExcludeCommands)] IEnumerable<IPathfindingRangeCommand<VertexModel>> excludeCommands,
            ILog logger)
        {
            pathfindingRange = this;
            this.service = service;
            this.includeCommands = includeCommands.OrderByOrderAttribute().ToReadOnly();
            this.excludeCommands = excludeCommands.OrderByOrderAttribute().ToReadOnly();
            this.logger = logger;
            messenger.Register<IsVertexInRangeRequest>(this, OnVertexIsInRangeRecieved);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<GraphActivatedMessage>(this, async (r, msg) => await OnGraphActivated(msg));
            AddToRangeCommand = ReactiveCommand.CreateFromTask<VertexModel>(AddVertexToRange);
            RemoveFromRangeCommand = ReactiveCommand.CreateFromTask<VertexModel>(RemoveVertexFromRange);
        }

        public IEnumerator<VertexModel> GetEnumerator()
        {
            return Transit.Prepend(Source).Append(Target).GetEnumerator();
        }

        private async Task AddVertexToRange(VertexModel vertex)
        {
            includeCommands.ExecuteFirst(pathfindingRange, vertex);
            await Task.CompletedTask;
        }

        private void SubcribeToEvents()
        {
            Transit.CollectionChanged += OnCollectionChanged;
            SubscribeOnRangeExtremumsRemoving(x => x.Source);
            SubscribeOnRangeExtremumsRemoving(x => x.Target);
            SubscribeOnRangeExtremumsAdding(x => x.Source);
            SubscribeOnRangeExtremumsAdding(x => x.Target);
        }

        private async Task AddRangeToStorage(VertexModel vertex)
        {
            await ExecuteSafe(async () =>
            {
                var range = (await service.ReadRangeAsync(GraphId))
                    .Select(x => (x.Id, x.VertexId))
                    .ToList();
                var vertices = pathfindingRange.ToList();
                var index = vertices.IndexOf(vertex);
                range.Insert(index, (Id: 0, VertexId: vertex.Id));
                var request = new UpsertPathfindingRangeRequest()
                {
                    GraphId = GraphId,
                    Ranges = range.Select((x, i) =>
                        (x.Id,
                        IsSource: i == 0,
                        IsTarget: i == range.Count - 1 && range.Count > 1,
                        x.VertexId,
                        Order: i))
                    .ToList()
                };
                await service.UpsertRangeAsync(request);
            }, logger.Error);
        }

        private async Task RemoveVertexFromRange(VertexModel vertex)
        {
            excludeCommands.ExecuteFirst(pathfindingRange, vertex);
            await Task.CompletedTask;
        }

        private async Task RemoveVertexFromStorage(VertexModel vertex)
        {
            await ExecuteSafe(async () =>
            {
                await service.DeleteRangeAsync(vertex.Enumerate());
            }, logger.Error);
        }

        private void SubscribeOnRangeExtremumsAdding(Expression<Func<PathfindingRangeViewModel, VertexModel>> expression)
        {
            this.WhenAnyValue(expression)
                .Skip(1)
                .Where(x => x != null)
                .Do(async x => await AddRangeToStorage(x))
                .Subscribe()
                .DisposeWith(disposables);
        }

        private void SubscribeOnRangeExtremumsRemoving(Expression<Func<PathfindingRangeViewModel, VertexModel>> expression)
        {
            this.WhenAnyValue(expression)
               .Buffer(2, 1)
               .Select(b => b[0])
               .Where(b => b != null)
               .Subscribe(async x => await RemoveVertexFromStorage(x))
               .DisposeWith(disposables);
        }

        private void ClearRange()
        {
            source = null;
            target = null;
            Transit.Clear();
        }

        private async void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var added = (VertexModel)e.NewItems[0];
                    await AddRangeToStorage(added);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removed = (VertexModel)e.OldItems[0];
                    await RemoveVertexFromStorage(removed);
                    break;
            }
        }

        private async Task OnGraphActivated(GraphActivatedMessage msg)
        {
            await ExecuteSafe(async () =>
            {
                disposables.Clear();
                Transit.CollectionChanged -= OnCollectionChanged;
                ClearRange();
                Graph = msg.Graph;
                GraphId = msg.GraphId;
                var range = await service.ReadRangeAsync(GraphId);
                var src = range.FirstOrDefault(x => x.IsSource);
                Source = src != null ? Graph.Get(src.Position) : null;
                var tgt = range.FirstOrDefault(x => x.IsTarget);
                Target = tgt != null ? Graph.Get(tgt.Position) : null;
                var transit = range.Where(x => !x.IsSource && !x.IsTarget)
                    .Select(x => Graph.Get(x.Position))
                    .ToList();
                Transit.AddRange(transit);
                SubcribeToEvents();
            }, logger.Error);
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(GraphId))
            {
                disposables.Clear();
                ClearRange();
                GraphId = 0;
            }
        }

        private void OnVertexIsInRangeRecieved(object recipient, IsVertexInRangeRequest request)
        {
            request.IsInRange = pathfindingRange.Contains(request.Vertex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}