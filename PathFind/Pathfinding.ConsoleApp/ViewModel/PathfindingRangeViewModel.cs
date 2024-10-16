﻿using Autofac.Features.AttributeFilters;
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
using static Terminal.Gui.View;

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
            set
            {
                var previous = source;
                this.RaiseAndSetIfChanged(ref source, value);
                if (previous != null && source == null)
                {
                    previous.IsSource = false;
                }
                else if (previous == null && source != null)
                {
                    source.IsSource = true;
                }
            }
        }

        private VertexModel target;
        public VertexModel Target
        {
            get => target;
            set
            {
                var previous = target;
                this.RaiseAndSetIfChanged(ref target, value);
                if (previous != null && target == null)
                {
                    previous.IsTarget = false;
                }
                else if (previous == null && target != null)
                {
                    target.IsTarget = true;
                }
            }
        }

        public ObservableCollection<VertexModel> Transit { get; } = new();

        IList<VertexModel> IPathfindingRange<VertexModel>.Transit => Transit;

        public ReactiveCommand<VertexModel, Unit> AddToRangeCommand { get; }

        public ReactiveCommand<VertexModel, Unit> RemoveFromRangeCommand { get; }

        public ReactiveCommand<MouseEventArgs, Unit> DeletePathfindingRange { get; }

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
            AddToRangeCommand = ReactiveCommand.Create<VertexModel>(AddVertexToRange);
            RemoveFromRangeCommand = ReactiveCommand.Create<VertexModel>(RemoveVertexFromRange);
            DeletePathfindingRange = ReactiveCommand.CreateFromTask<MouseEventArgs>(DeleteRange);
            Transit.ActOnEveryObject(OnTransitAdded, OnTransitRemoved);
        }

        public IEnumerator<VertexModel> GetEnumerator()
        {
            return Transit.Prepend(Source).Append(Target).GetEnumerator();
        }

        private void AddVertexToRange(VertexModel vertex)
        {
            includeCommands.ExecuteFirst(pathfindingRange, vertex);
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
                var range = (await Task.Run(()=> service.ReadRangeAsync(GraphId))
                    .ConfigureAwait(false))
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
                await Task.Run(() => service.UpsertRangeAsync(request))
                    .ConfigureAwait(false);
            }, logger.Error).ConfigureAwait(false);
        }

        private void RemoveVertexFromRange(VertexModel vertex)
        {
            excludeCommands.ExecuteFirst(pathfindingRange, vertex);
        }

        private async Task RemoveVertexFromStorage(VertexModel vertex)
        {
            await ExecuteSafe(async () => await Task.Run(() => service.DeleteRangeAsync(vertex.Enumerate())
                ).ConfigureAwait(false),
                logger.Error).ConfigureAwait(false);
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
            Source = null;
            Target = null;
            while (Transit.Count > 0) Transit.RemoveAt(0);
        }

        private async Task DeleteRange(MouseEventArgs args)
        {
            var result = await Task.Run(() => service.DeleteRangeAsync(GraphId))
                .ConfigureAwait(false);
            if (result)
            {
                ClearRange();
            }
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

        private void OnTransitAdded(VertexModel transit) => transit.IsTransit = true;

        private void OnTransitRemoved(VertexModel transit) => transit.IsTransit = false;

        private async Task OnGraphActivated(GraphActivatedMessage msg)
        {
            await ExecuteSafe(async () =>
            {
                disposables.Clear();
                Transit.CollectionChanged -= OnCollectionChanged;
                ClearRange();
                Graph = msg.Graph;
                GraphId = msg.GraphId;
                var range = await Task.Run(() => service.ReadRangeAsync(GraphId))
                    .ConfigureAwait(false);
                var src = range.FirstOrDefault(x => x.IsSource);
                Source = src != null ? Graph.Get(src.Position) : null;
                var tgt = range.FirstOrDefault(x => x.IsTarget);
                Target = tgt != null ? Graph.Get(tgt.Position) : null;
                var transit = range.Where(x => !x.IsSource && !x.IsTarget)
                    .Select(x => Graph.Get(x.Position))
                    .ToList();
                Transit.AddRange(transit);
                SubcribeToEvents();
            }, logger.Error).ConfigureAwait(false);
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