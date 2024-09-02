using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphTableViewModel : ReactiveObject
    {
        private readonly Lazy<ObservableCollection<GraphParametresModel>> graphs;
        private readonly IRequestService<VertexViewModel> service;
        private readonly IMessenger messenger;

        private GraphParametresModel activated;
        public GraphParametresModel Activated
        {
            get => activated;
            set
            {
                this.RaiseAndSetIfChanged(ref activated, value);
                messenger.Send(new GraphActivatedMessage(selected.Id));
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

        public ObservableCollection<GraphParametresModel> Graphs => graphs.Value;

        public GraphTableViewModel(
            IRequestService<VertexViewModel> service, 
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger)
        {
            graphs = new Lazy<ObservableCollection<GraphParametresModel>>(GetGraphs);
            this.service = service;
            this.messenger = messenger;
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<GraphDeletedMessage>(this, OnGraphDeleted);
        }

        private ObservableCollection<GraphParametresModel> GetGraphs()
        {
            var values = service.ReadAllGraphInfoAsync().Result.Select(x => new GraphParametresModel()
            {
                Id = x.Id,
                Name = x.Name,
                Width = x.Dimensions.ElementAt(0),
                Length = x.Dimensions.ElementAt(1),
                Obstacles = x.ObstaclesCount
            });
            return new ObservableCollection<GraphParametresModel>(values);
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
                activated = parametres;
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
