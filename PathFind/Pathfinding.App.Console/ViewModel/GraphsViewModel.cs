using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.ViewModel
{
    public class GraphsViewModel : ReactiveObject, ICanReceiveMessage
    {
        private readonly IRequestService<VertexViewModel> service;
        private readonly IMessenger messenger;
        private readonly Lazy<ObservableCollection<GraphInformationModel>> graphs;

        private GraphInformationModel currentGraph;
        public GraphInformationModel CurrentGraph 
        {
            get => currentGraph;
            set
            {
                this.RaiseAndSetIfChanged(ref currentGraph, value);
                var graph = service.ReadGraphAsync(currentGraph.Id).Result;
                // TODO: messenger.Send
            }
        }

        public ObservableCollection<GraphInformationModel> Graphs => graphs.Value;

        public GraphsViewModel(IRequestService<VertexViewModel> service,
            IMessenger messenger)
        {
            this.service = service;
            this.messenger = messenger;
            graphs = new(GetGraphs);
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            throw new System.NotImplementedException();
        }

        private ObservableCollection<GraphInformationModel> GetGraphs()
        {
            return new(service.ReadAllGraphInfoAsync().Result);
        }
    }
}
