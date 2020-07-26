using GraphLibrary.Vertex;

namespace GraphLibrary.Statistics
{
    public interface IStatisticsCollector
    {
        void StartCollect();
        void StopCollect();
        void Visited();
        void IncludeVertexInStatistics(IVertex vertex);
        Statistics GetStatistics();
    }
}
