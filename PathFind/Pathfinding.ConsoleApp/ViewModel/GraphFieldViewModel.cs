using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData.Tests;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Service.Interface.Requests.Update;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphFieldViewModel : ReactiveObject
    {
        private readonly IRequestService<VertexViewModel> service;
        private readonly IReadOnlyList<IPathfindingRangeCommand<VertexViewModel>> includeCommands;

        private int graphId;
        public int GraphId 
        {
            get => graphId;
            set => this.RaiseAndSetIfChanged(ref graphId, value);
        }

        private IGraph<VertexViewModel> graph;
        public IGraph<VertexViewModel> Graph
        {
            get => graph;
            set => this.RaiseAndSetIfChanged(ref graph, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> AddInRangeCommand { get; }

        public ReactiveCommand<MouseEventArgs, Unit> ReverseVertexCommand { get; }

        private ObservableCollection<VertexViewModel> PathfindingRange { get; }
            = new ObservableCollection<VertexViewModel>();

        public GraphFieldViewModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            IEnumerable<IPathfindingRangeCommand<VertexViewModel>> includeCommands,
            IRequestService<VertexViewModel> service)
        {
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<GraphDeletedMessage>(this, OnGraphDeleted);
            PathfindingRange.ActOnEveryObject(OnVertexAdded, OnVertexRemoved);
            this.service = service;
            this.includeCommands = includeCommands.OrderByOrderAttribute().ToReadOnly();
            AddInRangeCommand = ReactiveCommand.Create<MouseEventArgs>(AddToRange);
            ReverseVertexCommand = ReactiveCommand.Create<MouseEventArgs>(ReverseVertex);
        }

        private void AddToRange(MouseEventArgs e)
        {
            var vertex = (VertexViewModel)e.MouseEvent.View.Data;
            includeCommands.ExecuteFirst(PathfindingRange, vertex);
        }

        private void ReverseVertex(MouseEventArgs e)
        {
            var vertex = (VertexViewModel)e.MouseEvent.View.Data;
            if (!PathfindingRange.Contains(vertex))
            {
                vertex.IsObstacle = !vertex.IsObstacle;
                var request = new UpdateVerticesRequest<VertexViewModel>()
                {
                    GraphId = graphId,
                    Vertices = vertex.Enumerate().ToList()
                };
                var _ = service.UpdateVerticesAsync(request).Result;
            }
        }

        private void OnGraphCreated(object recipient, GraphCreatedMessage msg)
        {
            if (graphId == 0)
            {
                Graph = msg.Model.Graph;
                GraphId = msg.Model.Id;
            }
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            var model = service.ReadGraphAsync(msg.GraphId).Result;
            Graph = model.Graph;
            GraphId = model.Id;
            var range = service.ReadRangeAsync(GraphId).Result;
            PathfindingRange.Clear();
            PathfindingRange.AddRange(range.Range.Select(Graph.Get));
        }

        private void OnGraphDeleted(object recipient, GraphDeletedMessage msg)
        {
            if (msg.GraphId == GraphId)
            {
                GraphId = 0;
                Graph = Graph<VertexViewModel>.Empty;
                var _ = service.DeleteRangeAsync(PathfindingRange).Result;
                PathfindingRange.Clear();
            }
        }

        private void OnVertexAdded(VertexViewModel vertex)
        {
            var index = PathfindingRange.IndexOf(vertex);
            if (index == 0) vertex.VisualizeAsSource();
            else if (index == PathfindingRange.Count - 1) vertex.VisualizeAsTarget();
            else vertex.VisualizeAsTransit();
            var request = new CreatePathfindingRangeRequest<VertexViewModel>()
            {
                GraphId = graphId,
                Vertices = (index, vertex).Enumerate().ToList()
            };
            var _ = service.CreateRangeAsync(request).Result;
        }

        private void OnVertexRemoved(VertexViewModel vertex)
        {
            vertex.VisualizeAsRegular();
            var _ = service.DeleteRangeAsync(vertex.Enumerate()).Result;
        }
    }
}
