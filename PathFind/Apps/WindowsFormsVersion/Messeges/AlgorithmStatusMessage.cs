namespace WindowsFormsVersion.Messeges
{
    internal sealed class AlgorithmStatusMessage
    {
        public bool IsAlgorithmStarted { get; }

        public AlgorithmStatusMessage(bool isAlgorithmStarted)
        {
            IsAlgorithmStarted = isAlgorithmStarted;
        }
    }
}
