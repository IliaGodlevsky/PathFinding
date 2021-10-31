using Algorithm.Interfaces;

namespace WPFVersion.Messages
{
    internal sealed class ShowAlgorithmVisualization
    {
        public IAlgorithm Algorithm { get; }

        public ShowAlgorithmVisualization(IAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
