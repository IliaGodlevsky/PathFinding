using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedUnderAbscissa : FramedAbscissa
    {
        protected override int ValueOffset { get; }

        protected override int FrameOffset { get; }

        public FramedUnderAbscissa(IGraph<Vertex> graph)
            : base(graph.GetWidth())
        {
            ValueOffset = graph.GetLength() + 3;
            FrameOffset = graph.GetLength() + 1;
        }
    }
}