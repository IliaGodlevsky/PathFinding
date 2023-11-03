using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Visualizations.Containers
{
    internal abstract class VisualizedVertices : IVisualizedVertices
    {
        public event Action<int, Vertex> VertexVisualized;

        private readonly Dictionary<int, HashSet<Vertex>> vertices = new();

        public virtual bool Contains(int id, Vertex vertex)
        {
            return vertices.GetOrEmpty(id).Contains(vertex);
        }

        public virtual bool Add(int id, Vertex vertex)
        {
            bool added = vertices.TryGetOrAddNew(id).Add(vertex);
            VertexVisualized?.Invoke(id, vertex);
            return added;
        }

        public void Remove(int id, Vertex vertex)
        {
            vertices.GetOrEmpty(id).Remove(vertex);
        }

        public IReadOnlyCollection<Vertex> GetVertices(int id)
        {
            return vertices.GetOrEmpty(id);
        }

        public void Clear(int id)
        {
            vertices.Remove(id);
        }
    }
}