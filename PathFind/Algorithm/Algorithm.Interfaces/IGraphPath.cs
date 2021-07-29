using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Represents path, that is 
    /// found by pathfinding algorithm
    /// </summary>
    public interface IGraphPath
    {
        IVertex[] Path { get; }

        int PathLength { get; }

        double PathCost { get; }
    }
}
