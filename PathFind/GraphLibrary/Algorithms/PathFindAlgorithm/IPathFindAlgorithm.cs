using GraphLibrary.Collection;
using GraphLibrary.PauseMaker;

namespace GraphLibrary.Algorithm
{ 
    /// <summary>
    /// A base interface of path find algorithms
    /// </summary>
    public interface IPathFindAlgorithm
    {
        Graph Graph { get; set; }
        IPauseProvider Pauser { get; set; }
        void FindDestionation();
    }
}
