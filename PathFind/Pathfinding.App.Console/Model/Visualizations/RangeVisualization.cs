using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class RangeVisualization : IRangeVisualization<Vertex>
    {
        public ConsoleColor SourceVertexColor { get; set; } = ConsoleColor.Magenta;

        public ConsoleColor TargetVertexColor { get; set; } = ConsoleColor.Red;

        public ConsoleColor TransitVertexColor { get; set; } = ConsoleColor.Green;

        public bool IsVisualizedAsRange(Vertex visualizable)
        {
            return visualizable.Color.IsOneOf(SourceVertexColor, TargetVertexColor, TransitVertexColor);
        }

        public void VisualizeAsSource(Vertex visualizable)
        {
            visualizable.Color = SourceVertexColor;
        }

        public void VisualizeAsTarget(Vertex visualizable)
        {
            visualizable.Color = TargetVertexColor;
        }

        public void VisualizeAsTransit(Vertex visualizable)
        {
            visualizable.Color = TransitVertexColor;
        }
    }
}
