using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
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

        private GraphParametresModel selected;

        public GraphParametresModel Selected
        {
            get => selected;
            set
            {
                this.RaiseAndSetIfChanged(ref selected, value);
                //messenger.send
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
    }
}
