namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class StatisticsLineMessage
    {
        public string Value { get; }

        public StatisticsLineMessage(string value)
        {
            Value = value;
        }
    }
}
