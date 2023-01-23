using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Single;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.NullObjects
{
    public sealed class DeadEndVertex : Singleton<DeadEndVertex, IVertex>, IVertex
    {
        public bool IsObstacle
        {
            get => throw new DeadendVertexException();
            set => throw new DeadendVertexException();
        }

        public IVertexCost Cost
        {
            get => throw new DeadendVertexException();
            set => throw new DeadendVertexException();
        }

        public ICoordinate Position => throw new DeadendVertexException();

        public IList<IVertex> Neighbours
        {
            get => throw new DeadendVertexException();
            set => throw new DeadendVertexException();
        }

        public override bool Equals(object obj)
        {
            throw new DeadendVertexException();
        }

        public override int GetHashCode()
        {
            throw new DeadendVertexException();
        }

        public bool Equals(IVertex other)
        {
            throw new DeadendVertexException();
        }

        private DeadEndVertex()
        {

        }
    }
}
