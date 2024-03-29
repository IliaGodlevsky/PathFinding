﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedOverAbscissa : FramedAbscissa
    {
        protected override int ValueOffset { get; } = 0;

        protected override int FrameOffset { get; } = 0;


        public FramedOverAbscissa(IGraph<Vertex> graph)
            : base(graph.GetWidth())
        {
        }
    }
}
