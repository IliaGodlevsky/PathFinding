﻿namespace WindowsFormsVersion.Messeges
{
    internal sealed class UpdateStatisticsMessage
    {
        public string Statistics { get; }

        public UpdateStatisticsMessage(string statistics)
        {
            Statistics = statistics;
        }
    }
}