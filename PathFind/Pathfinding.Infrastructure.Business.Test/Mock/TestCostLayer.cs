﻿using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.Mock
{
    internal sealed class TestCostLayer : TestLayer<int>
    {
        private readonly InclusiveValueRange<int> range;

        protected override int[,] Matrix { get; } = new int[X, Y]
            {
                { 3, 7, 1, 9, 2, 8, 4, 5, 6, 3, 7, 1, 9, 2, 8, 4, 5, 6, 3, 7, 1, 9, 2, 8, 4, 5, 6, 3, 7, 1, 9, 2, 8, 4, 5 },
                { 6, 9, 3, 7, 1, 4, 2, 8, 5, 6, 9, 3, 7, 1, 4, 2, 8, 5, 6, 9, 3, 7, 1, 4, 2, 8, 5, 6, 9, 3, 7, 1, 4, 2, 8 },
                { 2, 5, 8, 6, 4, 7, 9, 1, 3, 2, 5, 8, 6, 4, 7, 9, 1, 3, 2, 5, 8, 6, 4, 7, 9, 1, 3, 2, 5, 8, 6, 4, 7, 9, 1 },
                { 7, 4, 6, 1, 9, 3, 5, 2, 8, 7, 4, 6, 1, 9, 3, 5, 2, 8, 7, 4, 6, 1, 9, 3, 5, 2, 8, 7, 4, 6, 1, 9, 3, 5, 2 },
                { 8, 2, 9, 5, 3, 7, 6, 4, 1, 8, 2, 9, 5, 3, 7, 6, 4, 1, 8, 2, 9, 5, 3, 7, 6, 4, 1, 8, 2, 9, 5, 3, 7, 6, 4 },
                { 4, 1, 7, 3, 8, 5, 2, 9, 6, 4, 1, 7, 3, 8, 5, 2, 9, 6, 4, 1, 7, 3, 8, 5, 2, 9, 6, 4, 1, 7, 3, 8, 5, 2, 9 },
                { 9, 8, 2, 4, 6, 1, 7, 3, 5, 9, 8, 2, 4, 6, 1, 7, 3, 5, 9, 8, 2, 4, 6, 1, 7, 3, 5, 9, 8, 2, 4, 6, 1, 7, 3 },
                { 5, 3, 4, 2, 7, 6, 8, 1, 9, 5, 3, 4, 2, 7, 6, 8, 1, 9, 5, 3, 4, 2, 7, 6, 8, 1, 9, 5, 3, 4, 2, 7, 6, 8, 1 },
                { 6, 9, 8, 5, 1, 4, 3, 7, 2, 6, 9, 8, 5, 1, 4, 3, 7, 2, 6, 9, 8, 5, 1, 4, 3, 7, 2, 6, 9, 8, 5, 1, 4, 3, 7 },
                { 3, 7, 1, 8, 2, 5, 9, 4, 6, 3, 7, 1, 8, 2, 5, 9, 4, 6, 3, 7, 1, 8, 2, 5, 9, 4, 6, 3, 7, 1, 8, 2, 5, 9, 4 },
                { 7, 2, 9, 6, 4, 3, 5, 8, 1, 7, 2, 9, 6, 4, 3, 5, 8, 1, 7, 2, 9, 6, 4, 3, 5, 8, 1, 7, 2, 9, 6, 4, 3, 5, 8 },
                { 4, 5, 6, 3, 7, 9, 2, 1, 8, 4, 5, 6, 3, 7, 9, 2, 1, 8, 4, 5, 6, 3, 7, 9, 2, 1, 8, 4, 5, 6, 3, 7, 9, 2, 1 },
                { 8, 6, 2, 7, 5, 4, 1, 9, 3, 8, 6, 2, 7, 5, 4, 1, 9, 3, 8, 6, 2, 7, 5, 4, 1, 9, 3, 8, 6, 2, 7, 5, 4, 1, 9 },
                { 9, 4, 5, 1, 3, 8, 7, 6, 2, 9, 4, 5, 1, 3, 8, 7, 6, 2, 9, 4, 5, 1, 3, 8, 7, 6, 2, 9, 4, 5, 1, 3, 8, 7, 6 },
                { 2, 1, 7, 9, 6, 3, 8, 5, 4, 2, 1, 7, 9, 6, 3, 8, 5, 4, 2, 1, 7, 9, 6, 3, 8, 5, 4, 2, 1, 7, 9, 6, 3, 8, 5 },
                { 5, 8, 3, 2, 4, 7, 1, 6, 9, 5, 8, 3, 2, 4, 7, 1, 6, 9, 5, 8, 3, 2, 4, 7, 1, 6, 9, 5, 8, 3, 2, 4, 7, 1, 6 },
                { 6, 7, 4, 1, 9, 2, 5, 3, 8, 6, 7, 4, 1, 9, 2, 5, 3, 8, 6, 7, 4, 1, 9, 2, 5, 3, 8, 6, 7, 4, 1, 9, 2, 5, 3 },
                { 7, 9, 6, 8, 3, 5, 4, 2, 1, 7, 9, 6, 8, 3, 5, 4, 2, 1, 7, 9, 6, 8, 3, 5, 4, 2, 1, 7, 9, 6, 8, 3, 5, 4, 2 },
                { 8, 3, 5, 7, 1, 9, 6, 4, 2, 8, 3, 5, 7, 1, 9, 6, 4, 2, 8, 3, 5, 7, 1, 9, 6, 4, 2, 8, 3, 5, 7, 1, 9, 6, 4 },
                { 9, 4, 1, 3, 6, 8, 7, 5, 2, 9, 4, 1, 3, 6, 8, 7, 5, 2, 9, 4, 1, 3, 6, 8, 7, 5, 2, 9, 4, 1, 3, 6, 8, 7, 5 },
                { 3, 2, 8, 6, 7, 4, 9, 1, 5, 3, 2, 8, 6, 7, 4, 9, 1, 5, 3, 2, 8, 6, 7, 4, 9, 1, 5, 3, 2, 8, 6, 7, 4, 9, 1 },
                { 1, 6, 2, 9, 5, 3, 8, 7, 4, 1, 6, 2, 9, 5, 3, 8, 7, 4, 1, 6, 2, 9, 5, 3, 8, 7, 4, 1, 6, 2, 9, 5, 3, 8, 7 },
                { 5, 8, 7, 4, 2, 1, 9, 6, 3, 5, 8, 7, 4, 2, 1, 9, 6, 3, 5, 8, 7, 4, 2, 1, 9, 6, 3, 5, 8, 7, 4, 2, 1, 9, 6 },
                { 4, 7, 9, 2, 3, 8, 5, 1, 6, 4, 7, 9, 2, 3, 8, 5, 1, 6, 4, 7, 9, 2, 3, 8, 5, 1, 6, 4, 7, 9, 2, 3, 8, 5, 1 },
                { 7, 5, 4, 8, 9, 2, 6, 3, 1, 7, 5, 4, 8, 9, 2, 6, 3, 1, 7, 5, 4, 8, 9, 2, 6, 3, 1, 7, 5, 4, 8, 9, 2, 6, 3 },
                { 2, 9, 3, 7, 6, 5, 1, 4, 8, 2, 9, 3, 7, 6, 5, 1, 4, 8, 2, 9, 3, 7, 6, 5, 1, 4, 8, 2, 9, 3, 7, 6, 5, 1, 4 },
                { 1, 3, 8, 9, 5, 7, 2, 6, 4, 1, 3, 8, 9, 5, 7, 2, 6, 4, 1, 3, 8, 9, 5, 7, 2, 6, 4, 1, 3, 8, 9, 5, 7, 2, 6 },
                { 6, 4, 7, 5, 2, 9, 8, 1, 3, 6, 4, 7, 5, 2, 9, 8, 1, 3, 6, 4, 7, 5, 2, 9, 8, 1, 3, 6, 4, 7, 5, 2, 9, 8, 1 },
                { 3, 6, 5, 8, 1, 4, 7, 2, 9, 3, 6, 5, 8, 1, 4, 7, 2, 9, 3, 6, 5, 8, 1, 4, 7, 2, 9, 3, 6, 5, 8, 1, 4, 7, 2 },
                { 9, 1, 2, 4, 7, 3, 6, 5, 8, 9, 1, 2, 4, 7, 3, 6, 5, 8, 9, 1, 2, 4, 7, 3, 6, 5, 8, 9, 1, 2, 4, 7, 3, 6, 5 }
            };

        public TestCostLayer()
        {
            range = new InclusiveValueRange<int>(9, 1);
        }

        protected override void Assign(IVertex vertex, int value)
        {
            vertex.Cost = new VertexCost(value, range);
        }
    }
}
