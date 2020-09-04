using GraphLibrary.Collection;
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
        Graph Graph { get; set; }
        IPauseProvider Pauser { get; set; }
        void FindDestionation();
    }
}
