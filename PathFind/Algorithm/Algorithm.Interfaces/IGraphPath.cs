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
        /// An array of vertices, that represents a found path.
        /// Vertices are arranged in order they were visited
        /// by a pathfinding algorithm
        /// </summary>
        IVertex[] Path { get; }

        /// <summary>
        /// A length of algorithms, or a number of steps that
        /// should be done to come from the source vertex to 
        /// the target one
        /// </summary>
        int Length { get; }

        /// <summary>
        /// A sum of step costs of vertices in the founded path
        /// </summary>
        double Cost { get; }
    }
}
