namespace WindowsFormsVersion.Messeges
{
    internal sealed class AlgorithmStatusMessage
    {
        public static AlgorithmStatusMessage Started => new AlgorithmStatusMessage(true);

        public static AlgorithmStatusMessage Finished => new AlgorithmStatusMessage(false);

        public bool IsAlgorithmStarted { get; }

        public AlgorithmStatusMessage(bool isAlgorithmStarted)
        {
            IsAlgorithmStarted = isAlgorithmStarted;
        }
    }
}
