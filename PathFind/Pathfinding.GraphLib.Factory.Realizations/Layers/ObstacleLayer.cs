using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using Shared.Random;
using Shared.Random.Extensions;
using System.Linq;

namespace Pathfinding.GraphLib.Factory.Realizations.Layers
{
    public sealed class ObstacleLayer<TGraph, TVertex> : ILayer<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly IRandom random;
        private readonly int obstaclePercent;

        public ObstacleLayer(IRandom random, int obstaclePercent)
        {
            this.random = random;
            this.obstaclePercent = obstaclePercent;
        }

        public void Overlay(TGraph graph)
        {
            int obstaclesCount = graph.Count * obstaclePercent / 100;
            int regularsCount = graph.Count - obstaclesCount;
            var obstacles = Enumerable.Repeat(true, obstaclesCount);
            var regulars = Enumerable.Repeat(false, regularsCount);
            var layer = obstacles.Concat(regulars).Shuffle(random.NextInt);
            graph.Zip(layer, CreateLayerItem).ForEach(SetObstacle);
        }

        private static (TVertex Vertex, bool Obstacle) CreateLayerItem(TVertex vertex, bool obstacle)
        {
            return (Vertex: vertex, Obstacle: obstacle);
        }

        private static void SetObstacle((TVertex Vertex, bool Obstacle) layerItem)
        {
            layerItem.Vertex.IsObstacle = layerItem.Obstacle;
        }
    }
}
