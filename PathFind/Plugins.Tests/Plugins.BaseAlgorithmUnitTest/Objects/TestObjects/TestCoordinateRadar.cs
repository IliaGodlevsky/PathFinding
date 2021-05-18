using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Plugins.BaseAlgorithmUnitTest.Objects.TestObjects
{
    internal sealed class TestCoordinateAroundRadar : ICoordinateRadar
    {
        public TestCoordinateAroundRadar(TestCoordinate coordinate)
        {
            this.coordinate = coordinate;
        }

        public IEnumerable<int[]> Environment
        {            
            get
            {
                if (environment == null)
                {
                    environment = FormEnvironment().ToList();
                }
                return environment;
            }
        }

        private IEnumerable<int[]> FormEnvironment()
        {
            int X = coordinate.X;
            int Y = coordinate.Y;
            for (int x = X - 1; x <= X + 1; x++)
            {
                for (int y = Y - 1; y <= Y + 1; y++)
                {
                    if (x != X || y != Y)
                    {
                        yield return new int[] { x, y };
                    }
                }
            }
        }

        private List<int[]> environment;
        private readonly TestCoordinate coordinate;
    }
}
