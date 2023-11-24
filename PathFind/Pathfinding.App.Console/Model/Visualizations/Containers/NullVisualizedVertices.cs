using Pathfinding.App.Console.Interface;
using Shared.Primitives.Single;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations.Containers
{
    internal sealed class NullVisualizedVertices : Singleton<NullVisualizedVertices, IVisualizedVertices>, IVisualizedVertices
    {
        public ICollection<IVisualizedVertices> Containers { get; }
            = Array.Empty<IVisualizedVertices>();

        public bool Contains(int id, Vertex vertex)
        {
            return false;
        }

        public bool Add(int id, Vertex vertex)
        {
            return false;
        }

        public void Remove(int id, Vertex vertex)
        {
            
        }

        public IReadOnlyCollection<Vertex> GetVertices(int id)
        {
            return Array.Empty<Vertex>();
        }

        public void Clear(int id) { }

        private NullVisualizedVertices()
        {

        }
    }
}
