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
        private int ObstaclePercent { get; }

        private IRandom Random { get; }

        public ObstacleLayer(IRandom random, int obstaclePercent)
        {
            ObstaclePercent = obstaclePercent;
            Random = random;
        }

        public void Overlay(TGraph graph)
        {
            int obstaclesCount = graph.Count * ObstaclePercent / 100;
            int regularsCount = graph.Count - obstaclesCount;
            var obstacles = Enumerable.Repeat(true, obstaclesCount);
            var regulars = Enumerable.Repeat(false, regularsCount);
            var layer = obstacles.Concat(regulars).Shuffle(Random.NextInt) .ToReadOnly();
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
