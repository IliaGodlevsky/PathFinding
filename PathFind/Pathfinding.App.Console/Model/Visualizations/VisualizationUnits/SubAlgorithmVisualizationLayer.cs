using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
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
                foreach (var subAlgorithm in algorithm.Algorithms)
                {
                    foreach (var vertex in subAlgorithm.Visited)
                    {
                        Speed?.Wait();
                        var current = (Vertex)graph.Get(vertex.Visited);
                        current.VisualizeAsVisited();
                        VisualizeAsEnqueued(vertex.Enqueued, graph);
                    }
                    VisualizeAsPath(subAlgorithm.Path, graph);
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

        private static void VisualizeAsPath(IEnumerable<ICoordinate> vertices,
            IGraph<IVertex> graph)
        {
            vertices.Select(graph.Get).OfType<Vertex>().VisualizeAsPath();
        }

        private static void VisualizeAsEnqueued(IEnumerable<ICoordinate> vertices,
            IGraph<IVertex> graph)
        {
            vertices.Select(graph.Get)
                .OfType<Vertex>().ForEach(x => x.VisualizeAsEnqueued());
        }
    }
}
