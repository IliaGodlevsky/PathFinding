using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Builders;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmUpdateViewModel : BaseViewModel, IAlgorithmUpdateViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog log;

        private RunInfoModel[] selected = Array.Empty<RunInfoModel>();
        private RunInfoModel[] Selected
        {
            get => selected;
            set => this.RaiseAndSetIfChanged(ref selected, value);
        }

        private GraphModel<GraphVertexModel> graph = GraphModel<GraphVertexModel>.Empty;
        private GraphModel<GraphVertexModel> Graph
        {
            get => graph;
            set => this.RaiseAndSetIfChanged(ref graph, value);
        }

        public ReactiveCommand<Unit, Unit> UpdateAlgorithmsCommand { get; }

        public AlgorithmUpdateViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog log)
        {
            this.messenger = messenger;
            this.service = service;
            this.log = log;
            UpdateAlgorithmsCommand = ReactiveCommand.CreateFromTask(ExecuteUpdate, CanUpdate());
            messenger.Register<RunSelectedMessage>(this, OnRunsSelected);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<GraphUpdatedMessage, int>(this, Tokens.AlgorithmUpdate, 
                async (r, msg) => await OnGraphUpdated(r, msg));
        }

        private void OnRunsSelected(object recipient, RunSelectedMessage msg)
        {
            Selected = msg.SelectedRuns;
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph;
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(Graph.Id))
            {
                Selected = Array.Empty<RunInfoModel>();
                Graph = null;
            }
        }

        private IObservable<bool> CanUpdate()
        {
            return this.WhenAnyValue(x => x.Selected,
                x => x.Graph, (s, g) => s.Length > 0 && g != null);
        }

        private async Task ExecuteUpdate()
        {
            await ExecuteSafe(async () =>
            {
                var models = await service.ReadStatisticsAsync(Selected.Select(x => x.Id))
                    .ConfigureAwait(false);
                var updated = await UpdateRunsAsync(models, Graph);
                messenger.Send(new RunsUpdatedMessage(updated));
            }, log.Error);
        }

        private async Task OnGraphUpdated(object recipient, GraphUpdatedMessage msg)
        {
            var graph = Graph;
            if ((graph != null && msg.Model.Id != graph.Id) || graph == null)
            {
                graph = await service.ReadGraphAsync(msg.Model.Id).ConfigureAwait(false);
                var layers = LayersBuilder.Take(graph).Build();
                await layers.OverlayAsync(graph.Graph).ConfigureAwait(false);
            }
            if (graph != null)
            {
                await ExecuteSafe(async () =>
                {
                    var models = await service.ReadStatisticsAsync(graph.Id).ConfigureAwait(false);
                    var updated = await UpdateRunsAsync(models, graph);
                    messenger.Send(new RunsUpdatedMessage(updated));
                }, log.Error);
            }
            msg.Signal();
        }

        private async Task<IReadOnlyCollection<RunStatisticsModel>> UpdateRunsAsync(
            IEnumerable<RunStatisticsModel> selected, 
            GraphModel<GraphVertexModel> graph)
        {
            var range = (await service.ReadRangeAsync(graph.Id).ConfigureAwait(false))
                .Select(x => graph.Graph.Get(x.Position))
                .ToList();
            var updatedRuns = new List<RunStatisticsModel>();
            if (range.Count > 1)
            {
                foreach (var select in selected)
                {
                    int visitedCount = 0;
                    void OnVertexProcessed(object sender, EventArgs e) => visitedCount++;
                    var info = await service.ReadStatisticAsync(select.Id).ConfigureAwait(false);
                    var algorithm = AlgorithmBuilder
                        .TakeAlgorithm(select.Algorithm)
                        .WithAlgorithmInfo(select)
                        .Build(range);
                    algorithm.VertexProcessed += OnVertexProcessed;

                    var status = RunStatuses.Success;
                    var path = NullGraphPath.Interface;
                    var stopwatch = Stopwatch.StartNew();
                    try
                    {
                        path = algorithm.FindPath();
                    }
                    catch
                    {
                        status = RunStatuses.Failure;
                    }

                    stopwatch.Stop();
                    algorithm.VertexProcessed -= OnVertexProcessed;

                    info.Elapsed = stopwatch.Elapsed;
                    info.Visited = visitedCount;
                    info.Cost = path.Cost;
                    info.Steps = path.Count;
                    info.ResultStatus = status;
                    updatedRuns.Add(info);
                }
                await ExecuteSafe(async () =>
                {
                    await service.UpdateStatisticsAsync(updatedRuns).ConfigureAwait(false);
                }, (ex, msg) => { 
                    log.Error(ex, msg); 
                    updatedRuns.Clear();
                });
            }
            return updatedRuns;
        }
    }
}
