using Algorithm.Interfaces;

namespace WPFVersion.Messages
{
    internal sealed class RemoveVisualizationMessage
    {
        public RemoveVisualizationMessage(IAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public IAlgorithm Algorithm { get; }
    }
}
