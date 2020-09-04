using GraphLibrary.Collection;
using System.Diagnostics;

namespace GraphLibrary.Statistics
{
    public interface IStatisticsCollector
    {
        void CollectStatistics(Graph graph, Stopwatch timer);
        string GetStatistics(string format);
    }
}
