using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal abstract class AlgorithmRunBaseViewModel : BaseViewModel
    {
        private readonly IGraphAssemble<RunVertexModel> graphAssemble;

        private IReadOnlyCollection<RunVertexModel> graphState = Array.Empty<RunVertexModel>();
        public IReadOnlyCollection<RunVertexModel> GraphState
        {
            get => graphState;
            set => this.RaiseAndSetIfChanged(ref graphState, value);
        }

        protected Queue<Action> Vertices { get; set; } = new();

        public int Remained => Vertices.Count;

        public ReactiveCommand<int, Unit> ProcessNextCommand { get; }

        protected AlgorithmRunBaseViewModel(IGraphAssemble<RunVertexModel> graphAssemble)
        {
            this.graphAssemble = graphAssemble;
            ProcessNextCommand = ReactiveCommand.Create<int>(ProcessNext);
        }

        protected Queue<Action> GetVerticesStates(IEnumerable<SubAlgorithmModel> subAlgorithms,
            IReadOnlyCollection<Coordinate> range,
            IGraph<RunVertexModel> graph)
        {
            var vertices = new Queue<Action>();
            range.Skip(1).Take(range.Count - 2)
                .ForEach(transit => vertices.Enqueue(() => graph.Get(transit).IsTransit = true));
            vertices.Enqueue(() => graph.Get(range.First()).IsSource = true);
            vertices.Enqueue(() => graph.Get(range.Last()).IsTarget = true);
            foreach (var sub in subAlgorithms)
            {
                foreach (var (Visited, Enqueued) in sub.Visited)
                {
                    vertices.Enqueue(() => graph.Get(Visited).IsVisited = true);
                    Enqueued.ForEach(enqueued => vertices.Enqueue(() => graph.Get(enqueued).IsEnqueued = true));
                }
                sub.Path.ForEach(path => vertices.Enqueue(() => graph.Get(path).IsPath = true));
            }
            return vertices;
        }

        private void ProcessNext(int number)
        {
            while (number-- > 0 && Remained > 0) Vertices.Dequeue().Invoke();
        }

        protected virtual async Task<IGraph<RunVertexModel>> CreateGraph(AlgorithmRunHistoryModel model)
        {
            var layer = new GraphStateLayer(model.GraphState);
            var graph = await graphAssemble.AssembleGraphAsync(layer, model.GraphInfo.Dimensions);
            return graph;
        }
    }
}
