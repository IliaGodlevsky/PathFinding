﻿using Pathfinding.App.Console.EventArguments;
using Pathfinding.Visualization.Core.Abstractions;
using System.Diagnostics;

namespace Pathfinding.App.Console.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class ConsoleVisualPathfindingRange : VisualPathfindingRange<Vertex>
    {
        protected override void SubscribeVertex(Vertex vertex)
        {
            vertex.IncludedInRange += SetPathfindingRange;
            vertex.MarkedAsIntermediateToReplace += MarkIntermediateVertexToReplace;
        }

        protected override void UnsubscribeVertex(Vertex vertex)
        {
            vertex.IncludedInRange -= SetPathfindingRange;
            vertex.MarkedAsIntermediateToReplace -= MarkIntermediateVertexToReplace;
        }

        private void SetPathfindingRange(object sender, VertexEventArgs e)
        {
            SetPathfindingRange(e.Current);
        }

        private void MarkIntermediateVertexToReplace(object sender, VertexEventArgs e)
        {
            MarkIntermediateVertexToReplace(e.Current);
        }
    }
}