using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model.Visualizations.Containers;
using Pathfinding.App.Console.Settings;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class VertexVisualizations
        : ITotalVisualization<Vertex>, ICanRecieveMessage, IDisposable
    {
        private readonly IReadOnlyDictionary<string, IVisualizedVertices> containers;

        private int CurrentGraph { get; set; }

        private Colours Colors { get; } = Colours.Default;

        public VertexVisualizations(IReadOnlyDictionary<string, IVisualizedVertices> containers)
        {
            this.containers = containers;
            Colors.PropertyChanged += OnPropertyChanged;
        }

        public bool IsVisualizedAsPath(Vertex vertex) => Contains(Constants.PathColorKeys, vertex);

        public bool IsVisualizedAsRange(Vertex vertex) => Contains(Constants.RangeColorKeys, vertex);

        public void VisualizeAsSource(Vertex vertex) => Visualize(vertex, Constants.SourceColorKey);

        public void VisualizeAsTarget(Vertex vertex) => Visualize(vertex, Constants.TargetColorKey);

        public void VisualizeAsTransit(Vertex vertex) => Visualize(vertex, Constants.TransitColorKey);

        public void VisualizeAsObstacle(Vertex vertex) => Visualize(vertex, Constants.ObstacleColorKey);

        public void VisualizeAsRegular(Vertex vertex) => Visualize(vertex, Constants.RegularColorKey);

        public void VisualizeAsVisited(Vertex vertex) => Visualize(vertex, Constants.VisitedColorKey);

        public void VisualizeAsEnqueued(Vertex vertex) => Visualize(vertex, Constants.EnqueuedColorKey);

        public void VisualizeAsPath(Vertex vertex)
        {
            if (!IsVisualizedAsRange(vertex))
            {
                string key = IsVisualizedAsPath(vertex)
                    ? Constants.CrossedPathColorKey
                    : Constants.PathColorKey;
                Visualize(vertex, key);
            }
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Visual, OnGraphCreated);
            messenger.Register<VertexVisualizations, GraphDeletedMessage>(this, Tokens.Common, OnGraphDeleted);
        }

        public void Dispose()
        {
            Colors.PropertyChanged -= OnPropertyChanged;
        }

        private void OnGraphDeleted(GraphDeletedMessage msg)
        {
            containers.Values.ForEach(c => c.Clear(msg.Id));
        }

        private void OnGraphCreated(GraphMessage msg)
        {
            CurrentGraph = msg.Graph.GetHashCode();
        }

        private void Visualize(Vertex vertex, string colorKey)
        {
            if (GetOrDefault(colorKey).Add(CurrentGraph, vertex))
            {
                vertex.Color = (ConsoleColor)Colors[colorKey];
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
            var color = (ConsoleColor)Colors[e.PropertyName];
            using (Cursor.HideCursor())
            {
                vertices.ForEach(vertex => vertex.Color = color);
            }
        }

        private bool Contains(string[] keys, Vertex vertex)
        {
            return keys.Select(GetOrDefault)
                .Any(vert => vert.Contains(CurrentGraph, vertex));
        }
    }
}