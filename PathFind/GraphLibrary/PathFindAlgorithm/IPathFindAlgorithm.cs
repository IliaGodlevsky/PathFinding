using GraphLibrary.PauseMaker;
using GraphLibrary.Statistics;

namespace GraphLibrary.Algorithm
{ 
    /// <summary>
    /// A base interface of path find algorithms
    /// </summary>
    public interface IPathFindAlgorithm
    {
        IStatisticsCollector StatCollector { get; set; }
        Pause PauseEvent { set; get; }
        bool FindDestionation();
        void DrawPath();
    }
}
