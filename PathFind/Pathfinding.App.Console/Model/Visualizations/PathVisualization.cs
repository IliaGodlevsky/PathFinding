using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class PathVisualization : IPathVisualization<Vertex>
    {
        public ConsoleColor PathVertexColor { get; set; } = ConsoleColor.DarkYellow;

        public ConsoleColor CrossedPathVertexColor { get; set; } = ConsoleColor.DarkRed;

        public bool IsVisualizedAsPath(Vertex visualizable)
        {
            return visualizable.Color.IsOneOf(PathVertexColor, CrossedPathVertexColor);
        }

        public void VisualizeAsPath(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange())
            {
                if (visualizable.IsVisualizedAsPath())
                {
                    visualizable.Color = CrossedPathVertexColor;
                }
                else
                {
                    visualizable.Color = PathVertexColor;
                }
            }
        }
    }
}
