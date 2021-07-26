using System;

namespace ConsoleVersion.EventArguments
{
    internal class StatisticsUpdatedEventArgs : EventArgs
    {
        public StatisticsUpdatedEventArgs(string statistics)
        {
            Statistics = statistics;
        }

        public string Statistics { get; }
    }
}
