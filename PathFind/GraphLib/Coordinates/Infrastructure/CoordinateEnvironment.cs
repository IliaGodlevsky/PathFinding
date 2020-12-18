using Common;
using GraphLib.Coordinates.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.ObjectActivator;

namespace GraphLib.Coordinates.Infrastructure
{
    public sealed class CoordinateEnvironment <TCoordinate> 
        where TCoordinate : ICoordinate
    {
        public CoordinateEnvironment(ICoordinate coordinate)
        {
            environment = new List<ICoordinate>();
            selfCoordinates = coordinate.Coordinates.ToArray();
            neighbourCoordinates = new int[selfCoordinates.Length];
            middleCoordinate = coordinate;
            limitDepth = selfCoordinates.Length;
        }

        static CoordinateEnvironment()
        {
            var ctor = typeof(TCoordinate).GetConstructor(new Type[] { typeof(int[]) });
            RegisterConstructor<ICoordinate>(ctor);
        }

        public IEnumerable<ICoordinate> GetEnvironment()
        {
            FormEnvironment();
            return environment;
        }

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
            var activator = (Activator<ICoordinate>)GetConstructor(typeof(TCoordinate));
            if (!NeighboursAreNegative)
            {
                var coordinate = activator(neighbourCoordinates);

                if (!middleCoordinate.Equals(coordinate))
                {
                    environment.Add(coordinate);
                }
            }
        }

        private bool NeighboursAreNegative
        {
            get
            {
                return neighbourCoordinates.Any(value => value < 0);
            }
        }

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

        private readonly ICoordinate middleCoordinate;

        private readonly int[] neighbourCoordinates;
        private readonly int[] selfCoordinates;

        private readonly List<ICoordinate> environment;
        private readonly int limitDepth;
    }
}
