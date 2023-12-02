using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.Visualization.Extensions
{
    public static class IVertexExtension
    {
        public static void RestoreDefaultVisualState<TVertex>(this TVertex self)
            where TVertex : IVertex, IGraphVisualizable
        {
            if (self.IsObstacle)
            {
                self.VisualizeAsObstacle();
            }
            else
            {
                self.VisualizeAsRegular();
            }
        }
    }
}