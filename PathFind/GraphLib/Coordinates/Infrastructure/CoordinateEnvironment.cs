using GraphLib.Coordinates.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates.Infrastructure
{
    public sealed class CoordinateEnvironment
    {
        public CoordinateEnvironment(ICoordinate coordinate)
        {
            selfCoordinates = coordinate.Coordinates.ToArray();
            neighbourCoordinates = new int[selfCoordinates.Length];
            coordinateType = coordinate.GetType();
            environment = new List<ICoordinate>();
            middleCoordinate = coordinate;
            limitDepth = selfCoordinates.Length;
        }

        public IEnumerable<ICoordinate> GetEnvironment()
        {
            FormEnvironment();
            return environment;
        }

        // recursive method
        private void FormEnvironment(int depth = 0)
        {
            for (int i = LeftNeighbour(depth); i <= RightNeighbour(depth); i++)
            {
                neighbourCoordinates[depth] = i;
                if (CanMoveDeeper(depth))
                    FormEnvironment(depth + 1);
                else
                    AddNeighbourToEnvironment();
            }
        }

        private void AddNeighbourToEnvironment()
        {
            if (!NeighboursAreNegative)
            {
                var coordinate = (ICoordinate)Activator.
                    CreateInstance(coordinateType, neighbourCoordinates);

                if (!middleCoordinate.Equals(coordinate))
                {
                    environment.Add(coordinate);
                }
            }
        }

        private bool NeighboursAreNegative => neighbourCoordinates.Any(value => value < 0);

        private int LeftNeighbour(int depth)
        {
            return selfCoordinates[depth] - 1;
        }

        private int RightNeighbour(int depth)
        {
            return LeftNeighbour(depth) + 2;
        }

        private bool CanMoveDeeper(int depth)
        {
            return depth < limitDepth - 1;
        }
       
        private readonly Type coordinateType;
        private readonly ICoordinate middleCoordinate;

        private readonly int[] neighbourCoordinates;
        private readonly int[] selfCoordinates;

        private readonly List<ICoordinate> environment;
        private readonly int limitDepth;
    }
}
