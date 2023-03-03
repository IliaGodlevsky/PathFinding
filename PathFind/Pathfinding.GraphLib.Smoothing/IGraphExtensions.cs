using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Smoothing.Interface;

namespace Pathfinding.GraphLib.Smoothing
{
    public static class IGraphExtensions
    {
        public static void Smooth<TVertex>(this IGraph<TVertex> self, IMeanCost meanCost, int smoothLevel = 1)
            where TVertex : IVertex
        {
            var layer = new SmoothLayer<IGraph<TVertex>, TVertex>(smoothLevel, meanCost);
            layer.Overlay(self);
        }
    }
}
