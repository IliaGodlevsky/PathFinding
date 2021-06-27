using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Algorithm.Сompanions
{
    public sealed class ParentVertices : IParentVertices
    {
        public ParentVertices()
        {
            parentVertices = new ConcurrentDictionary<ICoordinate, IVertex>();
        }

        public void Add(IVertex child, IVertex parent)
        {
            parentVertices[child.Position] = parent;
        }

        public IVertex GetParent(IVertex child)
        {
            return HasParent(child)
                ? parentVertices[child.Position]
                : new NullVertex();
        }

        public bool HasParent(IVertex child)
        {
            return parentVertices.TryGetValue(child.Position, out _);
        }

        public void Clear()
        {
            parentVertices.Clear();
        }

        private readonly IDictionary<ICoordinate, IVertex> parentVertices;
    }
}