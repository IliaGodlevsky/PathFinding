using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;

namespace Algorithm.Сompanions
{
    public sealed class ParentVertices : IParentVertices
    {
        private readonly Dictionary<ICoordinate, IVertex> parentVertices;

        public ParentVertices()
        {
            parentVertices = new Dictionary<ICoordinate, IVertex>();
        }

        public void Add(IVertex child, IVertex parent)
        {
            if (!parent.IsNull() && !child.IsNull())
            {
                parentVertices[child.Position] = parent;
            }
        }

        public IVertex GetParent(IVertex child)
        {
            return HasParent(child)
                ? parentVertices[child.Position]
                : throw new KeyNotFoundException();
        }

        public bool HasParent(IVertex child)
        {
            return parentVertices.TryGetValue(child.Position, out _);
        }

        public void Clear()
        {
            parentVertices.Clear();
        }
    }
}