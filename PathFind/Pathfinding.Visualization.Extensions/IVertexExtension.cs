using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.Visualization.Extensions
{
    public static class IVertexExtension
    {
        public static void RestoreDefaultVisualState<TVertex>(this TVertex self)
            where TVertex : IVertex, IVisualizable
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

        public static void Initialize<TVertex>(this TVertex self)
            where TVertex : IVertex, IVisualizable
        {
            (self as IVertex).InitializeComponents();
            self.RestoreDefaultVisualState();
        }
    }
}