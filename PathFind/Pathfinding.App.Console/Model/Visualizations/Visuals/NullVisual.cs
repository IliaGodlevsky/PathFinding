using Pathfinding.App.Console.Interface;
using Shared.Primitives.Single;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class NullVisual : Singleton<NullVisual, IVisual>, IVisual
    {
#pragma warning disable CS0067
        public event Action<Vertex> Visualized;
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

        private NullVisual()
        {

        }
    }
}
