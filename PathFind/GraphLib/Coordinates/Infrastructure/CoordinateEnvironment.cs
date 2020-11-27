using GraphLib.Coordinates.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates.Infrastructure
{
    internal sealed class CoordinateEnvironment
    {
        public CoordinateEnvironment(ICoordinate coordinate)
        {
            middleCoordinate = coordinate;
            coordinateType = coordinate.GetType();
            selfCoordinates = coordinate.Coordinates.ToArray();
            neighbourCoordinates = new int[selfCoordinates.Length];
        }

        public IEnumerable<ICoordinate> GetEnvironment()
        {
            return GetNeighbours(currentDepth: 0, limit: selfCoordinates.Count());
        }

        private IEnumerable<ICoordinate> GetNeighbours(int currentDepth, int limit)
        {
            var environment = new List<ICoordinate>();
            if (currentDepth < limit)
            {
                int start = selfCoordinates[currentDepth] - 1;
                int end = selfCoordinates[currentDepth] + 1;
                for (int i = start; i <= end; i++)
                {
                    neighbourCoordinates[currentDepth] = i;
                    if (CanMoveNextDimension(currentDepth, limit))
                    {
                        var temp = GetNeighbours(currentDepth + 1, limit);
                        environment.AddRange(temp);
                    }
                    else
                        GetNeighbours(environment);
                }
            }
            return environment;
        }

        private void GetNeighbours(List<ICoordinate> environment)
        {
            if (!neighbourCoordinates.Any(value => value < 0))
            {
                var coordinate = CreateCoordinate();

                if (!middleCoordinate.Equals(coordinate))
                {
                    environment.Add(coordinate);
                }
            }
        }

        private ICoordinate CreateCoordinate()
        {
            return (ICoordinate)Activator.
                CreateInstance(coordinateType, neighbourCoordinates);
        }

        private bool CanMoveNextDimension(int currentDepth, int limitDepth)
        {
            return currentDepth < limitDepth - 1;
        }

        private readonly Type coordinateType;
        private readonly ICoordinate middleCoordinate;

        private readonly int[] neighbourCoordinates;
        private readonly int[] selfCoordinates;
    }
}
