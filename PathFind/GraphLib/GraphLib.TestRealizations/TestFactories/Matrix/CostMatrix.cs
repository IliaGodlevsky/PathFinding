﻿using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories.Matrix
{
    internal sealed class CostMatrix : BaseMatrix<int>
    {
        public CostMatrix(Graph2D graph) : base(graph)
        {

        }

        protected override void Assign(IVertex vertex, int value)
        {
            vertex.Cost = new TestVertexCost(value);
        }

        protected override int[,] CreateMatrix()
        {
            return new int[Constants.Width, Constants.Length]
            {
                {1,5,8,3,6,1,3,3,8,6,1,6,6,8,2,7,3,8,8,4},
                {6,4,8,1,8,3,7,4,6,3,6,4,1,8,8,4,1,1,2,6},
                {5,4,2,7,3,3,8,3,8,6,1,5,7,5,3,2,1,7,4,2},
                {5,2,2,6,2,2,1,3,8,8,3,7,6,5,3,1,6,7,4,3},
                {7,6,8,1,6,2,6,5,2,6,7,2,7,5,1,6,7,4,7,7},
                {4,4,5,2,6,3,8,8,1,1,2,2,8,3,5,5,2,8,5,8},
                {7,3,2,7,6,8,7,8,8,7,2,1,8,5,6,7,8,5,8,2},
                {2,3,1,3,8,5,3,2,5,3,2,6,2,1,3,3,5,7,6,8},
                {5,3,7,1,1,6,3,7,3,1,2,7,7,7,8,1,7,7,4,6},
                {6,7,7,6,5,7,3,4,8,4,6,8,1,6,8,7,1,4,3,5},
                {1,8,5,3,7,7,1,8,1,6,1,2,8,1,5,8,7,1,8,3},
                {2,1,3,3,7,7,6,1,2,7,8,7,6,3,5,5,4,2,5,1},
                {1,4,3,8,1,7,7,6,7,8,3,5,7,7,2,7,1,6,7,5},
                {1,1,5,6,8,7,4,4,5,8,1,2,7,4,4,1,3,4,5,8},
                {5,1,2,8,5,6,2,3,3,5,4,5,4,8,1,1,6,1,4,3},
                {7,8,2,5,2,1,5,4,5,5,8,2,3,5,5,3,6,4,4,5},
                {7,7,5,2,8,5,8,6,8,6,2,4,5,6,5,7,5,1,7,2},
                {6,8,6,4,5,1,6,7,4,6,1,2,2,1,8,3,2,4,3,8},
                {1,2,1,7,5,7,3,6,4,8,8,5,8,7,6,6,3,8,2,7},
                {1,6,5,1,7,5,3,2,8,8,1,7,8,5,1,7,8,6,1,4},
                {2,8,8,2,5,7,7,8,3,3,1,3,4,4,2,5,1,1,4,6},
                {6,6,6,4,6,3,8,1,1,6,1,8,1,8,3,3,5,8,2,5},
                {1,7,3,6,6,1,3,3,8,3,6,3,7,3,5,4,5,2,1,8},
                {6,4,2,3,2,2,1,5,5,4,3,6,2,8,4,5,3,5,4,1},
                {6,5,5,7,7,4,1,2,5,4,5,6,8,3,1,6,7,1,8,8},
            };
        }
    }
}
