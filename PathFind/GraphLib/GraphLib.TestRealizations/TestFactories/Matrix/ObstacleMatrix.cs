using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;

namespace GraphLib.TestRealizations.TestFactories.Matrix
{
    internal sealed class ObstacleMatrix : BaseMatrix<bool>, IMatrix
    {
        private const bool O = true;
        private const bool I = false;

        public ObstacleMatrix(Graph2D graph) : base(graph)
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
                {I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,O},
                {I,I,I,O,I,I,O,O,I,I,I,O,I,I,I,O,I,I,I,I},
                {I,O,O,I,I,I,I,I,I,O,I,O,I,I,O,O,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,O,I,I,O,I,I,I,I,I,O,I},
                {I,O,I,O,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I},
                {I,I,I,I,I,I,O,O,O,I,I,I,I,O,I,I,I,I,I,I},
                {I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,O,I,O,I,O,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,O,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,O},
                {I,I,I,I,O,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,O,O,I,I,I,O,I,I,I,I,I,I,I,I,O},
                {I,O,I,I,I,I,I,I,I,O,I,O,I,I,I,O,I,I,O,I},
                {I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I},
                {I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,O,O,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I},
            };
        }
    }
}
