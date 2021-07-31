using System;

namespace WindowsFormsVersion.EventArguments
{
    internal class StatisticsChangedEventArgs : EventArgs
    {
        public StatisticsChangedEventArgs(string statistics)
        {
            Statistics = statistics;
        }

        public string Statistics { get; }
    }
}
