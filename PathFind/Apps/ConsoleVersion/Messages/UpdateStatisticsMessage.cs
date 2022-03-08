namespace ConsoleVersion.Messages
{
    internal sealed class UpdateStatisticsMessage
    {
        public static UpdateStatisticsMessage Empty => new UpdateStatisticsMessage(string.Empty);

        public string Statistics { get; }

        public UpdateStatisticsMessage(string statistics)
        {
            Statistics = statistics;
        }
    }
}