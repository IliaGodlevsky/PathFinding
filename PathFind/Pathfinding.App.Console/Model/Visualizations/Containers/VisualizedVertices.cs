using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Visualizations.Containers
{
    internal class VisualizedVertices : IVisualizedVertices, IDisposable
    {
        private readonly Dictionary<int, HashSet<Vertex>> vertices = new();

        public ICollection<IVisualizedVertices> Containers { get; }

        public virtual bool Contains(int id, Vertex vertex)
        {
            return vertices.GetOrEmpty(id).Contains(vertex);
        }

        public VisualizedVertices()
        {
            Containers = new HashSet<IVisualizedVertices>();
        }

        public virtual bool Add(int id, Vertex vertex)
        {
            if (vertices.TryGetOrAddNew(id).Add(vertex))
            {
                foreach (var container in Containers)
                {
                    container.Remove(id, vertex);
                }
                return true;
            }
            return false;
        }

        public void Remove(int id, Vertex vertex)
        {
            vertices.GetOrEmpty(id).Remove(vertex);
        }

        public IReadOnlyCollection<Vertex> GetVertices(int id)
        {
            return vertices.GetOrEmpty(id).ToReadOnly();
        }

        public void Clear(int id)
        {
            vertices.Remove(id);
        }

        public void Dispose()
        {
            Containers.Clear();
        }
    }
}