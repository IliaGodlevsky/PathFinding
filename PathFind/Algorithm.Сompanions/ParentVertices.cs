using System.Collections.Generic;
using GraphLib.Common.NullObjects;
using GraphLib.Interfaces;

namespace Algorithm.Сompanions
{
    public sealed class ParentVertices
    {
        public ParentVertices()
        {
            parentVertices = new Dictionary<ICoordinate, IVertex>();
        }

        public void Add(IVertex child, IVertex parent)
        {
            parentVertices[child.Position] = parent;
        }

        public IVertex GetParent(IVertex child)
        {
            if (HasParent(child))
            {
                return parentVertices[child.Position];
            }

            return new DefaultVertex();
        }

        public bool HasParent(IVertex child)
        {
            return parentVertices.TryGetValue(child.Position, out _);
        }

        public void Clear()
        {
            parentVertices.Clear();
        }

        private readonly Dictionary<ICoordinate, IVertex> parentVertices;
    }
}