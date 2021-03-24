using GraphLib.Interface;
using System.Collections.Generic;

namespace Algorithm.Realizations.Tests.TestObjects
{
    internal sealed class TestVertex : IVertex
    {
        public bool IsObstacle { get; set; }
        public IVertexCost Cost { get; set; }
        public IList<IVertex> Neighbours { get; set; }
        public ICoordinate Position { get; set; }
    }
}
