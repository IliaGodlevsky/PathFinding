using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using Shared.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class SubAlgorithmVisualizationLayer : VisualizationLayer
    {
        private readonly ConsoleKeystrokesHook hook = new();
        private readonly TimeSpan? initSpeed;

        private TimeSpan? Speed { get; set; }

        public SubAlgorithmVisualizationLayer(RunVisualizationDto algorithm) 
            : base(algorithm)
        {
            initSpeed = algorithm.AlgorithmSpeed;
            Speed = initSpeed;
        }

        private void Loose()
        {
            Speed = Speed is null ? initSpeed : null;
        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            using (Disposable.Use(StopHook))
            {
                hook.KeyPressed += OnKeyPressed;
                Task.Run(hook.Start);
                var subAlgorithms = algorithm.Algorithms.OrderBy(x => x.Order);
                foreach (var subAlgorithm in subAlgorithms)
                {
                    foreach (var vertex in subAlgorithm.Visited)
                    {
                        Speed?.Wait();
                        var current = (Vertex)graph.Get(vertex.Visited);
                        current.VisualizeAsVisited();
                        vertex.Enqueued.Select(graph.Get)
                            .OfType<Vertex>().ForEach(x => x.VisualizeAsEnqueued());
                    }
                    subAlgorithm.Path.Select(graph.Get)
                        .OfType<Vertex>().VisualizeAsPath();
                }
            }
        }

        private void StopHook()
        {
            hook.KeyPressed -= OnKeyPressed;
            hook.Interrupt();
        }

        private void OnKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            switch (e.PressedKey)
            {
                case ConsoleKey.Enter:
                    Loose();
                    break;
            }
        }
    }
}
