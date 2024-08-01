using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Shared.Extensions;
using Shared.Random;
using System.Linq;

using static System.Linq.Enumerable;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class ObstacleLayer : ILayer
    {
        private readonly IRandom random;
        private readonly int obstaclePercent;

        public ObstacleLayer(IRandom random, int obstaclePercent)
        {
            this.random = random;
            this.obstaclePercent = obstaclePercent;
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            int obstaclesCount = graph.Count * obstaclePercent / 100;
            int regularsCount = graph.Count - obstaclesCount;
            Repeat(true, obstaclesCount)
               .Concat(Repeat(false, regularsCount))
               .OrderBy(item => random.NextUInt()) // shuffle
               .Zip(graph, (o, v) => (Vertex: v, Obstacle: o))
               .ForEach(item => item.Vertex.IsObstacle = item.Obstacle);
        }
    }
}
