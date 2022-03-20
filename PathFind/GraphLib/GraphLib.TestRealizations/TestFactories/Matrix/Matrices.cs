namespace GraphLib.TestRealizations.TestFactories.Matrix
{
    internal sealed class Matrices : IMatrix
    {
        private readonly IMatrix[] matrices;

        public Matrices(params IMatrix[] matrices)
        {
            this.matrices = matrices;
        }

        public void Overlay()
        {
            foreach (var matrix in matrices)
            {
                matrix.Overlay();
            }
        }
    }
}
