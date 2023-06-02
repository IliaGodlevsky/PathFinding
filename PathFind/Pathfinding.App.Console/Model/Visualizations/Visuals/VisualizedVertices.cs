using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal abstract class VisualizedVertices : IVisualizedVertices
    {
        public event Action<Vertex> VertexVisualized;

        private readonly HashSet<Vertex> vertices = new();

        private ConsoleColor Color => (ConsoleColor)Colours.Default[SettingsKey];

        protected abstract string SettingsKey { get; }

        protected VisualizedVertices()
        {
            Colours.Default.PropertyChanged += OnSettingsChaged;
        }

        public virtual bool Contains(Vertex vertex)
        {
            return vertices.Contains(vertex);
        }

        public virtual void Visualize(Vertex vertex)
        {
            vertex.Color = Color;
            vertices.Add(vertex);
            VertexVisualized?.Invoke(vertex);
        }

        public void Remove(Vertex vertex)
        {
            vertices.Remove(vertex);
        }

        private void OnSettingsChaged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(SettingsKey))
            {
                foreach (var vertex in vertices)
                {
                    vertex.Color = Color;
                }
            }
        }
    }
}