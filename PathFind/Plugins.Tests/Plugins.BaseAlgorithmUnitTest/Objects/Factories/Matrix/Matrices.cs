namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories.Matrix
{
    internal sealed class Matrices : IMatrix
    {
        public Matrices(params IMatrix[] matrices)
        {
            this.matrices = matrices;
        }

        public void Overlay()
        {
            foreach(var matrix in matrices)
            {
                matrix.Overlay();
            }
        }

        private readonly IMatrix[] matrices;
    }
}
