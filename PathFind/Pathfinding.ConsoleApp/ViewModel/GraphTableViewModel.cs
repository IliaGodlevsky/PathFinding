using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
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

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphTableViewModel : ReactiveObject
    {
        private readonly IRequestService<VertexModel> service;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        private GraphParametresModel activated;
        public GraphParametresModel Activated
        {
            get => activated;
            set
            {
                this.RaiseAndSetIfChanged(ref activated, value);
                try
                {
                    var graph = service.ReadGraphAsync(activated.Id).GetAwaiter().GetResult();
                    messenger.Send(new GraphActivatedMessage(graph.Id, graph.Graph));
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
        }

        private GraphParametresModel selected;

        public GraphParametresModel Selected
        {
            get => selected;
            set
            {
                this.RaiseAndSetIfChanged(ref selected, value);
                messenger.Send(new GraphSelectedMessage(selected.Id));
            }
        }

        public ObservableCollection<GraphParametresModel> Graphs { get; } = new();

        public GraphTableViewModel(
            IRequestService<VertexModel> service, 
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.messenger = messenger;
            this.logger = logger;
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<GraphDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<ObstaclesCountChangedMessage>(this, OnObstaclesCountChanged);
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

        private List<GraphParametresModel> GetGraphs()
        {
            try
            {
                return service.ReadAllGraphInfoAsync()
                .GetAwaiter()
                .GetResult()
                .Select(x => new GraphParametresModel()
                {
                    Id = x.Id,
                    Name = x.Name,
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

        private void OnGraphCreated(object recipient, GraphCreatedMessage msg)
        {
            var parametres = new GraphParametresModel
            {
                Id = msg.Model.Id,
                Name = msg.Model.Name,
                Obstacles = msg.Model.Graph.GetObstaclesCount(),
                Width = msg.Model.Graph.GetWidth(),
                Length = msg.Model.Graph.GetLength()
            };
            Graphs.Add(parametres);
            if (Graphs.Count == 1)
            {
                Activated = parametres;
            }
        }

        private void OnGraphDeleted(object recipient, GraphDeletedMessage msg)
        {
            var graph = Graphs.FirstOrDefault(x => x.Id == msg.GraphId);
            if (graph != null)
            {
                Graphs.Remove(graph);
            }
        }
    }
}
