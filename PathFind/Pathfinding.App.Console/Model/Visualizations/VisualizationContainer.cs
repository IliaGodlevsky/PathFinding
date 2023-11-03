using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model.Visualizations.Containers;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class VisualizationContainer : ITotalVisualization<Vertex>, ICanRecieveMessage, IDisposable
    {
        private readonly IReadOnlyDictionary<string, IVisualizedVertices> containers;

        private int CurrentGraph { get; set; }

        private Colours Colors { get; } = Colours.Default;

        public VisualizationContainer(IReadOnlyDictionary<string, IVisualizedVertices> containers)
        {
            this.containers = containers;
            CommunicateContainers(containers.Values);
            Colors.PropertyChanged += OnPropertyChanged;
        }

        public bool IsVisualizedAsPath(Vertex visualizable)
        {
            return GetOrDefault(Constants.CrossedPathColorKey).Contains(CurrentGraph, visualizable)
                || GetOrDefault(Constants.PathColorKey).Contains(CurrentGraph, visualizable);
        }

        public bool IsVisualizedAsRange(Vertex visualizable)
        {
            return GetOrDefault(Constants.SourceColorKey).Contains(CurrentGraph, visualizable)
                || GetOrDefault(Constants.TargetColorKey).Contains(CurrentGraph, visualizable)
                || GetOrDefault(Constants.TransitColorKey).Contains(CurrentGraph, visualizable);
        }

        public void VisualizeAsPath(Vertex visualizable)
        {
            if (!IsVisualizedAsRange(visualizable))
            {
                if (!IsVisualizedAsPath(visualizable))
                {
                    Visualize(visualizable, Constants.PathColorKey);
                }
                else
                {
                    Visualize(visualizable, Constants.CrossedPathColorKey);
                }
            }
        }

        public void VisualizeAsSource(Vertex visualizable) => Visualize(visualizable, Constants.SourceColorKey);

        public void VisualizeAsTarget(Vertex visualizable) => Visualize(visualizable, Constants.TargetColorKey);

        public void VisualizeAsTransit(Vertex visualizable) => Visualize(visualizable, Constants.TransitColorKey);

        public void VisualizeAsObstacle(Vertex vertex) => Visualize(vertex, Constants.ObstacleColorKey);

        public void VisualizeAsRegular(Vertex vertex) => Visualize(vertex, Constants.ReguularColorKey);

        public void VisualizeAsVisited(Vertex vertex) => Visualize(vertex, Constants.VisitedColorKey);

        public void VisualizeAsEnqueued(Vertex vertex) => Visualize(vertex, Constants.EnqueuedColorKey);

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Visual, OnGraphCreated);
            messenger.Register<GraphDeletedMessage>(this, Tokens.Common, OnGraphDeleted);
        }

        public void Dispose()
        {
            Colors.PropertyChanged -= OnPropertyChanged;
        }

        private void OnGraphDeleted(GraphDeletedMessage msg)
        {
            containers.Values.ForEach(c => c.Clear(msg.Id));
        }

        private void OnGraphCreated(IGraph<Vertex> graph)
        {
            CurrentGraph = graph.GetHashCode();
        }

        private void Visualize(Vertex vertex, string colorKey)
        {
            if (GetOrDefault(colorKey).Add(CurrentGraph, vertex))
            {
                var color = (ConsoleColor)Colors[colorKey];
                vertex.Color = color;
            }
        }

        private IVisualizedVertices GetOrDefault(string colorKey)
        {
            return containers.GetOrDefault(colorKey, NullVisualizedVertices.Instance);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var container = GetOrDefault(e.PropertyName);
            var vertices = container.GetVertices(CurrentGraph);
            foreach (var vertex in vertices)
            {
                vertex.Color = (ConsoleColor)Colors[e.PropertyName];
            }
        }

        private static void CommunicateContainers(IEnumerable<IVisualizedVertices> containers)
        {
            foreach (var outerVisual in containers)
            {
                var except = containers.Except(outerVisual);
                foreach (var innerVisual in except)
                {
                    outerVisual.VertexVisualized += innerVisual.Remove;
                }
            }
        }
    }
}