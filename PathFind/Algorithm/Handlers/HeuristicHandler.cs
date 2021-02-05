using GraphLib.Interface;

namespace Algorithm.Handlers
{
    /// <summary>
    /// A handler for a heuristic function for heuristic pathfinding algorithms
    /// </summary>
    /// <param name="vertex"></param>
    public delegate double HeuristicHandler(IVertex vertex);
}
