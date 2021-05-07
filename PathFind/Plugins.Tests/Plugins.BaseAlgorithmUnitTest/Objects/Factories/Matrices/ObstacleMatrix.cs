using GraphLib.Interfaces;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories.Matrices
{
    internal sealed class ObstacleMatrix : BaseMatrix<bool>
    {
        private const bool O = true;
        private const bool I = false;

        public ObstacleMatrix(IGraph graph)
            : base(graph)
        {
            matrix = new bool[Constants.Width, Constants.Length]
            {
                {I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,O,O,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,O,I,I,O,O,I,I,I,O,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,O},
                {I,O,O,I,I,I,I,I,I,O,I,O,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,O,I,I,O,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I},
                {I,O,I,O,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,O},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,O,I,I,I,O,I,I,O,O,I,O,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,O,O,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,O,I,O,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,O,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,O,O,I,I,I,I,O,O,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I},
                {I,I,I,I,O,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,O,I,I,O,I,I,I},
                {I,I,I,I,I,O,O,I,I,I,O,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,O,I,I,I,I,I,I,O,O},
                {I,O,I,I,I,I,I,I,I,O,I,O,I,I,I,O,I,I,O,I,I,I,O,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,O,I},
                {I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,O,I,I,O,I,I,I},
                {I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,O,O,I,I,I,I,I,I,I,O,I,I,O,I},
                {I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,O,O,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,O,I,O,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,O},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O},
                {I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,O,O,I,I,I,I,I,I},
                {O,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,O,I,O},
                {I,I,I,I,I,I,O,I,I,I,I,I,O,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,O,I,I,I,I,I,I},
                {O,I,I,I,I,I,O,I,O,I,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,O,I,I,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,O,I,I,I,I,I,I,I,I,I,O,O,I,O,O,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,O,I,I,O,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I},
                {I,I,I,O,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,O,I,I,I,I,I,I,I},
                {I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,O,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,O,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I},
                {I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,O,I,I,O,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,O,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I},
                {O,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,O,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,O,I},
                {O,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,O,I,I,I,I,I,I,O,O,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {O,I,I,I,O,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,O,I,I,O,O,I,I,I,O,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,O},
                {I,I,I,I,I,I,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,O},
                {I,I,I,I,I,O,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,O},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O},
                {I,I,I,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I,O,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,O,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,O,I,O},
                {I,O,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I},
                {I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,O,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,O,O,I,I,I,O,I,I,O,I,I,I},
                {O,O,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,O,I,I,I,I,O,I,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I},
                {I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I},
                {O,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,O,I,I,I,I,I,I,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,O,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,O},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,O,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,O,I,I,I,I,I,I,O,I},
                {I,I,I,I,I,I,I,I,O,I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,O,I,I,O,I,I,I,I,I,O,I,O,I,I,I,I},
                {I,I,I,I,I,O,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,I,I},
                {I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,O,I,I,O,I,I,I,I,I},
                {O,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,O,O,I,O,I,I,I,I,I},
                {O,O,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I},
                {I,I,I,I,I,I,I,O,I,I,I,I,I,O,O,I,O,I,I,I,I,I,I,I,O,I,I,O,I,I,I,I,I,O,I,I,I,I,O,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,O,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I},
                {I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,I,I,O,I,I,I,I,I},
                {I,I,I,I,I,O,I,I,I,I,O,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,I,I,I,O,I,O,I,I,I,I,I,I},
                {I,I,I,I,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,O,O,I,O,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I,I}
            };
        }

        protected override void Assign(IVertex vertex, bool value)
        {
            vertex.IsObstacle = value;
        }
    }
}
