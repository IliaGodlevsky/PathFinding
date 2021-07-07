using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Represents path, that is 
    /// found by pathfinding algorithm
    /// </summary>
    public interface IGraphPath
    {
        /// <summary>
        /// An array of vertices that make 
        /// up the path from the source 
        /// vertex to the target one
        /// </summary>
        IVertex[] Path { get; }

        int PathLength { get; }

        double PathCost { get; }
    }
}
