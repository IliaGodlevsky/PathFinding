using System;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class VisualizationPair
    {
        public RunVertexModel Vertex { get; set; }

        public Action<RunVertexModel> Action { get; set; }
    }
}
