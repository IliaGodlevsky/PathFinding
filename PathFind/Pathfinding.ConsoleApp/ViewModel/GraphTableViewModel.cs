using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphTableViewModel : BaseViewModel
    {
        private readonly IRequestService<GraphVertexModel> service;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        private GraphInfoModel[] selectedGraphs;

        public GraphInfoModel[] SelectedGraphs
        {
            get => selectedGraphs;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedGraphs, value);
                var toSend = selectedGraphs.Select(x => x.Id).ToArray();
                messenger.Send(new GraphSelectedMessage(toSend));
            }
        }

        public ReactiveCommand<GraphInfoModel, Unit> ActivateGraphCommand { get; }

        public ObservableCollection<GraphInfoModel> Graphs { get; } = new();

        public GraphTableViewModel(
            IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.messenger = messenger;
            this.logger = logger;
            messenger.Register<GraphCreatedMessage>(this, async (r, msg) => await OnGraphCreated(r, msg));
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<ObstaclesCountChangedMessage>(this, OnObstaclesCountChanged);
            ActivateGraphCommand = ReactiveCommand.CreateFromTask<GraphInfoModel>(ActivatedGraph);
        }

        private async Task ActivatedGraph(GraphInfoModel model)
        {
            await ExecuteSafe(async () =>
            {
                var graph = await service.ReadGraphAsync(model.Id);
                messenger.Send(new GraphActivatedMessage(graph.Id, graph.Graph));
            }, logger.Error);
        }

        public void LoadGraphs()
        {
            foreach (var value in GetGraphs())
            {
                Graphs.Add(value);
            }
        }

        private void OnObstaclesCountChanged(object recipient, ObstaclesCountChangedMessage msg)
        {
            var graph = Graphs.FirstOrDefault(x => x.Id == msg.GraphId);
            if (graph != null)
            {
                graph.Obstacles += msg.Delta;
            }
        }

        private List<GraphInfoModel> GetGraphs()
        {
            try
            {
                return service.ReadAllGraphInfoAsync()
                    .GetAwaiter()
                    .GetResult()
                    .Select(x => new GraphInfoModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Neighborhood = x.Neighborhood,
                        SmoothLevel = x.SmoothLevel,
                        Width = x.Dimensions.ElementAt(0),
                        Length = x.Dimensions.ElementAt(1),
                        Obstacles = x.ObstaclesCount
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new();
            }
        }

        private async Task OnGraphCreated(object recipient, GraphCreatedMessage msg)
        {
            if (msg.Models.Length > 0)
            {
                var parametres = msg.Models
                    .Select(x =>
                        new GraphInfoModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Obstacles = x.Graph.GetObstaclesCount(),
                            Width = x.Graph.GetWidth(),
                            Length = x.Graph.GetLength(),
                            SmoothLevel = x.SmoothLevel,
                            Neighborhood = x.Neighborhood
                        })
                    .ToArray();
                Graphs.Add(parametres);
                if (Graphs.Count == 1)
                {
                    await ActivatedGraph(Graphs.First());
                }
            }
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            var graphs = Graphs
                .Where(x => msg.GraphIds.Contains(x.Id))
                .ToList();
            Graphs.Remove(graphs);

        }
    }
}
