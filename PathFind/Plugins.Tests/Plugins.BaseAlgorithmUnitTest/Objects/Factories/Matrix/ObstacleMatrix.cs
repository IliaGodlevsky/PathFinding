using GraphLib.Interfaces;
using Plugins.BaseAlgorithmUnitTest.Objects.TestObjects;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories.Matrix
{
    internal sealed class ObstacleMatrix : BaseMatrix<bool>
    {
        private const bool O = true;
        private const bool I = false;

        public ObstacleMatrix(TestGraph graph)
            : base(graph)
        {
            matrix = new bool[,]
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
