using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories.Matrix
{
    internal sealed class ObstacleMatrix : BaseMatrix<bool>
    {
        private const bool X = true;
        private const bool O = false;

        public ObstacleMatrix(Graph2D<TestVertex> graph) : base(graph)
        {

        }

        protected override void Assign(IVertex vertex, bool value)
        {
            vertex.IsObstacle = value;
        }

        protected override bool[,] CreateMatrix()
        {
            return new bool[Constants.Width, Constants.Length]
            {
                {O,O,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,X,O,O,O,O,O,X,O,O,O,X},
                {O,O,O,X,O,O,X,X,O,O,O,X,O,O,O,X,O,O,O,O},
                {O,X,X,O,O,O,O,O,O,X,O,X,O,O,X,X,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,X,O,O,X,O,O,O,O,O,X,O},
                {O,X,O,X,O,O,O,O,O,O,X,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,X,O,O,O},
                {O,O,O,O,O,O,X,X,X,O,O,O,O,X,O,O,O,O,O,O},
                {O,X,O,O,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,X,O,X,O,X,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,X,X,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,X,O,O,O,O,O,X,O,O,O,X,O,O,O,O,O,O,X},
                {O,O,O,O,X,O,O,O,O,O,O,X,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,X,X,O,O,O,X,O,O,O,O,O,O,O,O,X},
                {O,X,O,O,O,O,O,O,O,X,O,X,O,O,O,X,O,O,X,O},
                {O,O,X,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,X,O,O,O,X,O,O,O,O,O,O},
                {O,O,O,O,O,O,X,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,X,O,O,O,O,O,O,O,X,X,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,X,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O},
            };
        }
    }
}
