using GraphLibrary.Vertex;
using System.Collections.Generic;

namespace GraphLibrary.Statistics
{
    public interface IStatisticsCollector
    {
        void StartCollect();
        void StopCollect();
        void Visited();
        void IncludeVertexInStatistics(IVertex vertex);
        void IncludeVerticesInStatistics(IEnumerable<IVertex> collection);
        string GetStatistics(string format);
    }
}
