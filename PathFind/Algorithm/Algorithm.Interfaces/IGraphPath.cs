using GraphLib.Interfaces;

namespace Algorithm.Interfaces
{
    public interface IGraphPath
    {
        IVertex[] Path { get; }

        int PathLength { get; }

        double PathCost { get; }
    }
}
