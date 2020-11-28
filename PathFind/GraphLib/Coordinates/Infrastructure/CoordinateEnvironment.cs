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
            selfCoordinates = coordinate.Coordinates.ToArray();
            neighbourCoordinates = new int[selfCoordinates.Length];
            coordinateType = coordinate.GetType();
            environment = new List<ICoordinate>();
            middleCoordinate = coordinate;
        }

        public IEnumerable<ICoordinate> GetEnvironment()
        {
            int limitDepth = selfCoordinates.Count();
            FormEnvironment(currentDepth: 0, limitDepth);
            return environment;
        }

        // recursive method
        private void FormEnvironment(int currentDepth, int limitDepth)
        {
            if (currentDepth < limitDepth)
            {
                int leftNeighbour = selfCoordinates[currentDepth] - 1;
                int rightNeighbour = selfCoordinates[currentDepth] + 1;
                for (int i = leftNeighbour; i <= rightNeighbour; i++)
                {
                    neighbourCoordinates[currentDepth] = i;
                    if (CanMoveNextDimension(currentDepth, limitDepth))
                        FormEnvironment(currentDepth + 1, limitDepth);
                    else
                        AddNeighbourCoordinateToEnvironment();
                }
            }
        }

        private void AddNeighbourCoordinateToEnvironment()
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

        private readonly List<ICoordinate> environment;
    }
}
