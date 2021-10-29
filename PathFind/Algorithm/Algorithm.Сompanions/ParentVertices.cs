using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Extensions;
using System.Collections.Generic;

namespace Algorithm.Сompanions
{
    public sealed class ParentVertices : IParentVertices
    {
        public ParentVertices()
        {
            parentVertices = new Dictionary<ICoordinate, IVertex>();
        }

        public void Add(IVertex child, IVertex parent)
        {
            if (parent.IsNull() || child.IsNull())
            {
                return;
            }
            parentVertices[child.Position] = parent;
        }

        public IVertex GetParent(IVertex child)
        {
            return HasParent(child)
                ? parentVertices[child.Position]
                : NullVertex.Instance;
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