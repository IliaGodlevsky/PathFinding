using Pathfinding.App.Console.Interface;
using Shared.Primitives.Single;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class NullVisualizedVertices : Singleton<NullVisualizedVertices, IVisualizedVertices>, IVisualizedVertices
    {
#pragma warning disable CS0067
        public event Action<Vertex> VertexVisualized;
#pragma warning restore CS0067

        public bool Contains(Vertex vertex)
        {
            return false;
        }

        public void Remove(Vertex vertex)
        {

        }

        public void Visualize(Vertex vertex)
        {

        }

        public IEnumerator<Vertex> GetEnumerator()
        {
            return Enumerable.Empty<Vertex>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private NullVisualizedVertices()
        {

        }
    }
}
