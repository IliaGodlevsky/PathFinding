﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Extensions;
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
    internal sealed class PathfindingRangeViewModel : BaseViewModel, 
        IPathfindingRange<GraphVertexModel>, IPathfindingRangeViewModel
    {
        private readonly CompositeDisposable disposables = new();
        private readonly IRequestService<GraphVertexModel> service;
        private readonly IEnumerable<IPathfindingRangeCommand<GraphVertexModel>> includeCommands;
        private readonly IEnumerable<IPathfindingRangeCommand<GraphVertexModel>> excludeCommands;
        private readonly IPathfindingRange<GraphVertexModel> pathfindingRange;
        private readonly ILog logger;

        private int GraphId { get; set; }

        private IGraph<GraphVertexModel> Graph { get; set; }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get => isReadOnly;
            set => this.RaiseAndSetIfChanged(ref isReadOnly, value);
        }

        private GraphVertexModel source;
        public GraphVertexModel Source
        {
            get => source;
            set
            {
                var previous = source;
                source = value;
                if (value == null && previous != null)
                {
                    previous.IsSource = false;
                }
                else if (value != null)
                {
                    value.IsSource = true;
                }
            }
        }

        private GraphVertexModel target;
        public GraphVertexModel Target
        {
            get => target;
            set
            {
                var previous = target;
                target = value;
                if (value == null && previous != null)
                {
                    previous.IsTarget = false;
                }
                else if (value != null)
                {
                    value.IsTarget = true;
                }
            }
        }

        public ObservableCollection<GraphVertexModel> Transit { get; } = new();

        IList<GraphVertexModel> IPathfindingRange<GraphVertexModel>.Transit => Transit;

        public ReactiveCommand<GraphVertexModel, Unit> AddToRangeCommand { get; }

        public ReactiveCommand<GraphVertexModel, Unit> RemoveFromRangeCommand { get; }

        public ReactiveCommand<Unit, Unit> DeletePathfindingRange { get; }

        public PathfindingRangeViewModel(
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.IncludeCommands)] IEnumerable<IPathfindingRangeCommand<GraphVertexModel>> includeCommands,
            [KeyFilter(KeyFilters.ExcludeCommands)] IEnumerable<IPathfindingRangeCommand<GraphVertexModel>> excludeCommands,
            ILog logger)
        {
            pathfindingRange = this;
            this.service = service;
            this.includeCommands = includeCommands.OrderByOrderAttribute().ToReadOnly();
            this.excludeCommands = excludeCommands.OrderByOrderAttribute().ToReadOnly();
            this.logger = logger;
            messenger.Register<IsVertexInRangeRequest>(this, OnVertexIsInRangeRecieved);
            messenger.Register<QueryPathfindingRangeMessage>(this, OnGetPathfindingRangeRecieved);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<GraphStateChangedMessage>(this, OnGraphBecameReadonly);
            messenger.Register<AsyncGraphActivatedMessage, int>(this, Tokens.PathfindingRange,
                async (r, msg) => await OnGraphActivated(msg));
            AddToRangeCommand = ReactiveCommand.Create<GraphVertexModel>(AddVertexToRange, CanExecute());
            RemoveFromRangeCommand = ReactiveCommand.Create<GraphVertexModel>(RemoveVertexFromRange, CanExecute());
            DeletePathfindingRange = ReactiveCommand.CreateFromTask(DeleteRange, CanExecute());
        }

        private IObservable<bool> CanExecute()
        {
            return this.WhenAnyValue(x => x.IsReadOnly, isRead => !isRead);
        }

        public IEnumerator<GraphVertexModel> GetEnumerator()
        {
            return Transit.Prepend(Source).Append(Target).GetEnumerator();
        }

        private void AddVertexToRange(GraphVertexModel vertex)
        {
            includeCommands.ExecuteFirst(pathfindingRange, vertex);
        }

        private void BindTo(Expression<Func<GraphVertexModel, bool>> expression,
            GraphVertexModel model)
        {
            model.WhenAnyValue(expression)
                .Skip(1)
                .Where(x=> x)
                .Do(async _ => await AddRangeToStorage(model))
                .Subscribe()
                .DisposeWith(disposables);
            model.WhenAnyValue(expression)
                .Skip(1)
                .Where(x => !x)
                .Do(async x => await RemoveVertexFromStorage(model))
                .Subscribe()
                .DisposeWith(disposables);
        }

        private void SubcribeToEvents(GraphVertexModel vertex)
        {
            BindTo(x => x.IsSource, vertex);
            BindTo(x => x.IsTarget, vertex);
            BindTo(x => x.IsTransit, vertex);
        }

        private async Task AddRangeToStorage(GraphVertexModel vertex)
        {
            await ExecuteSafe(async () =>
            {
                var vertices = pathfindingRange.ToList();
                var index = vertices.IndexOf(vertex);
                await service.CreatePathfindingVertexAsync(GraphId, vertex.Id, index)
                    .ConfigureAwait(false);
            }, logger.Error).ConfigureAwait(false);
        }

        private void RemoveVertexFromRange(GraphVertexModel vertex)
        {
            excludeCommands.ExecuteFirst(pathfindingRange, vertex);
        }

        private async Task RemoveVertexFromStorage(GraphVertexModel vertex)
        {
            await ExecuteSafe(async ()
                => await service.DeleteRangeAsync(vertex.Enumerate()).ConfigureAwait(false),
                logger.Error).ConfigureAwait(false);
        }

        private void ClearRange()
        {
            Source = null;
            Target = null;
            while (Transit.Count > 0) Transit.RemoveAt(0);
        }

        private async Task DeleteRange()
        {
            var result = await service.DeleteRangeAsync(GraphId).ConfigureAwait(false);
            if (result)
            {
                ClearRange();
            }
        }

        private async Task OnGraphActivated(AsyncGraphActivatedMessage msg)
        {
            await ExecuteSafe(async () =>
            {
                disposables.Clear();
                Transit.CollectionChanged -= OnCollectionChanged;
                ClearRange();
                Graph = new Graph<GraphVertexModel>(msg.Graph.Vertices,
                    msg.Graph.DimensionSizes);
                GraphId = msg.Graph.Id;
                IsReadOnly = msg.Graph.Status == GraphStatuses.Readonly;
                var range = await service.ReadRangeAsync(GraphId).ConfigureAwait(false);
                var src = range.FirstOrDefault(x => x.IsSource);
                Source = src != null ? Graph.Get(src.Position) : null;
                var tgt = range.FirstOrDefault(x => x.IsTarget);
                Target = tgt != null ? Graph.Get(tgt.Position) : null;
                var transit = range.Where(x => !x.IsSource && !x.IsTarget)
                    .Select(x => Graph.Get(x.Position))
                    .ToList();
                Transit.CollectionChanged += OnCollectionChanged;
                Transit.AddRange(transit);
                Graph.ForEach(SubcribeToEvents);
            }, logger.Error).ConfigureAwait(false);
            msg.Signal(Unit.Default);
        }

        private void OnGraphBecameReadonly(object recipient, GraphStateChangedMessage msg)
        {
            IsReadOnly = msg.Status == GraphStatuses.Readonly;
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(GraphId))
            {
                disposables.Clear();
                ClearRange();
                GraphId = 0;
                IsReadOnly = false;
                Graph = Graph<GraphVertexModel>.Empty;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var added = (GraphVertexModel)e.NewItems[0];
                    added.IsTransit = true;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removed = (GraphVertexModel)e.OldItems[0];
                    removed.IsTransit = false;
                    break;
            }
        }

        private void OnVertexIsInRangeRecieved(object recipient, IsVertexInRangeRequest request)
        {
            request.IsInRange = pathfindingRange.Contains(request.Vertex);
        }

        private void OnGetPathfindingRangeRecieved(object recipient, QueryPathfindingRangeMessage msg)
        {
            msg.PathfindingRange = pathfindingRange
                .Where(x => x is not null)
                .Select(x => x.Position)
                .ToList()
                .AsReadOnly();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}