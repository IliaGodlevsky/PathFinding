using GraphLib.Vertex.Interface;

namespace Algorithm.Handlers
{
    /// <summary>
    /// A handler for a heuristic function for heuristic pathfinding algorithms
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns>A corrected cost of vertex</returns>
    public delegate double HeuristicHandler(IVertex vertex);
}
