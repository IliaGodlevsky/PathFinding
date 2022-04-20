using GraphLib.Interfaces;

namespace ConsoleVersion.Messages
{
    internal sealed class ClaimGraphAnswer
    {
        public IGraph Graph { get; }

        public ClaimGraphAnswer(IGraph graph)
        {
            Graph = graph;
        }
    }
}
