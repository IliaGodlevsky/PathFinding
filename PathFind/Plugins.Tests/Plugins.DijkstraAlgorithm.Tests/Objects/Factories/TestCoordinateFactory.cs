using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.DijkstraAlgorithm.Tests.Objects.TestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.DijkstraAlgorithm.Tests.Objects.Factories
{
    internal sealed class TestCoordinateFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(int[] coordinates)
        {
            return new TestCoordinate(coordinates);
        }
    }
}
