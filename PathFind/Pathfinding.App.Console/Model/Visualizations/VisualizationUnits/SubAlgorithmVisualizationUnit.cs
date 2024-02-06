using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using Shared.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class SubAlgorithmVisualizationUnit : VisualizationUnit
    {
        private readonly TimeSpan millisecond = TimeSpan.FromMilliseconds(1);
        private readonly ConsoleKeystrokesHook hook = new();
        private readonly TimeSpan? initSpeed;

        private TimeSpan? Speed { get; set; }

        public SubAlgorithmVisualizationUnit(AlgorithmReadDto algorithm) 
            : base(algorithm)
        {
            initSpeed = algorithm.Statistics.AlgorithmSpeed;
            Speed = initSpeed;
        }

        private void Loose()
        {
            Speed = Speed is null ? initSpeed : null;
        }

        public override void Visualize(IGraph<Vertex> graph)
        {
            using (Disposable.Use(StopHook))
            {
                hook.KeyPressed += OnKeyPressed;
                Task.Run(hook.Start);
                var subAlgorithms = algorithm
                    .SubAlgorithms
                    .OrderBy(x => x.Order);
                foreach (var subAlgorithm in subAlgorithms)
                {
                    foreach (var vertex in subAlgorithm.Visited)
                    {
                        Speed?.Wait();
                        var current = graph.Get(vertex.Visited);
                        current.VisualizeAsVisited();
                        foreach (var item in vertex.Enqueued)
                        {
                            var enqueued = graph.Get(item);
                            enqueued.VisualizeAsEnqueued();
                        }
                    }
                    foreach (var coordinate in subAlgorithm.Path)
                    {
                        var vertex = graph.Get(coordinate);
                        vertex.VisualizeAsPath();
                    }
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
