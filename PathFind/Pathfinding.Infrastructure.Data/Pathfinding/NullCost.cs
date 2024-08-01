﻿using Pathfinding.Domain.Interface;
using Shared.Primitives.Single;
using Shared.Primitives.ValueRange;
using System.Diagnostics;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    [DebuggerDisplay("Null")]
    public sealed class NullCost : Singleton<NullCost, IVertexCost>, IVertexCost
    {
        public int CurrentCost
        {
            get => default;
            set { }
        }

        public InclusiveValueRange<int> CostRange
        {
            get => default;
            set { }
        }

        private NullCost()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is NullCost;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Empty;
        }

        public IVertexCost SetCost(int cost)
        {
            return Instance;
        }
    }
}
