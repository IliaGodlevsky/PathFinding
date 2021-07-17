namespace GraphLib.Interfaces
{
    /// <summary>
    /// An interface, that provides 
    /// methods and properties 
    /// for start and end vertices
    /// for pathfinding process
    /// </summary>
    public interface IEndPoints
    {
        /// <summary>
        /// A vertex, to what the path should be found
        /// </summary>
        IVertex Target { get; }

        /// <summary>
        /// A vertex, from what the path should be found
        /// </summary>
        IVertex Source { get; }

        /// <summary>
        /// Checks, whether <paramref name="vertex"/>
        /// is <see cref="Target"/> or <see cref="Source"/>
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        bool IsEndPoint(IVertex vertex);
    }
}
