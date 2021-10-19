namespace Algorithm.Interfaces
{
    /// <summary>
    /// An interface for all pathfinding algorithms
    /// </summary>
    public interface IAlgorithm
    {
        /// <summary>
        /// Finds path in the graph 
        /// or in the other data structure
        /// </summary>
        /// <returns></returns>
        IGraphPath FindPath();
    }
}
