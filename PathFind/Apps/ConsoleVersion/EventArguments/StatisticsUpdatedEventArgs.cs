using System;

namespace ConsoleVersion.EventArguments
{
    internal class StatisticsUpdatedEventArgs : EventArgs
    {
        public string Statistics { get; }

        public StatisticsUpdatedEventArgs(string statistics)
        {
            Statistics = statistics;
        }
    }
}
