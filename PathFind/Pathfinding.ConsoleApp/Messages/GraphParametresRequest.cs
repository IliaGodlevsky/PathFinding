using Pathfinding.Domain.Interface.Factories;

namespace Pathfinding.ConsoleApp.Messages
{
    internal sealed class GraphParametresRequest
    {
        public INeighborhoodFactory NeighborhoodFactory { get; set; }

        public string Name { get; set; }

        public int SmoothLevel { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int Obstacles { get; set; }
    }
}
