using Common.Extensions;
using GraphLib.Coordinates.Abstractions;
using System.Collections.Generic;
using System.Linq;
using static Common.ObjectActivator;

namespace GraphLib.Coordinates.Infrastructure
{
    public sealed class CoordinateEnvironment<TCoordinate>
        where TCoordinate : class, ICoordinate
    {
        public CoordinateEnvironment(TCoordinate coordinate)
        {
            environment = new List<TCoordinate>();
            selfCoordinates = coordinate.Coordinates.ToArray();
            neighbourCoordinates = new int[selfCoordinates.Length];
            middleCoordinate = coordinate;
            limitDepth = selfCoordinates.Length;
        }

        static CoordinateEnvironment()
        {
            var ctor = typeof(TCoordinate).GetConstructor(typeof(int[]));
            RegisterConstructor<TCoordinate>(ctor);
        }

        public IEnumerable<ICoordinate> GetEnvironment()
        {
            FormEnvironment();
            return environment;
        }

        private void FormEnvironment(int depth = 0)
        {
            foreach (var i in GetNeighbourCoordinates(depth))
            {
                neighbourCoordinates[depth] = i;
                if (CanMoveDeeper(depth))
                {
                    FormEnvironment(depth + 1);
                }
                else
                {
                    AddNeighbourToEnvironment();
                }
            }
        }

        private void AddNeighbourToEnvironment()
        {
            var activator = GetActivator<TCoordinate>();
            var coordinate = activator(neighbourCoordinates);

            if (!middleCoordinate.Equals(coordinate))
            {
                environment.Add(coordinate);
            }
        }

        private IEnumerable<int> GetNeighbourCoordinates(int depth)
        {
            return new int[]
            {
                selfCoordinates[depth] - 1,
                selfCoordinates[depth],
                selfCoordinates[depth] + 1
            };
        }

        private bool CanMoveDeeper(int depth)
        {
            return depth < limitDepth - 1;
        }

        private readonly TCoordinate middleCoordinate;

        private readonly int[] neighbourCoordinates;
        private readonly int[] selfCoordinates;

        private readonly List<TCoordinate> environment;
        private readonly int limitDepth;
    }
}
