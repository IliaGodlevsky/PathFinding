using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValueRange;
using ValueRange.Extensions;

namespace GraphLib.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsCardinal(this IReadOnlyCollection<int> centralCoordinates, IReadOnlyCollection<int> cardinalCoordinates)
        {
            // Cardinal coordinate differs from central coordinate only for one coordinate value
            return centralCoordinates.Count == cardinalCoordinates.Count
                ? centralCoordinates.Zip(cardinalCoordinates, (x, y) => x != y).Count(i => i) == 1
                : false;
        }

        public static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection, Func<IVertex, bool> predicate)
        {
            return collection.FirstOrDefault(predicate) ?? NullVertex.Instance;
        }

        public static T VisualizeAsPath<T>(this T path)
            where T : IEnumerable<IVertex>
        {
            path.OfType<IVisualizable>()
                .Reverse()
                .Where(vertex => !vertex.IsVisualizedAsEndPoint)
                .ForEach(vertex => vertex.VisualizeAsPath());
            return path;
        }

        public static async Task<T> VisualizeAsPathAsync<T>(this T path)
            where T : IEnumerable<IVertex>
        {
            return await Task.Run(() => path.VisualizeAsPath()).ConfigureAwait(false);
        }

        public static int[] ToCoordinates(this int[] dimensionSizes, int index)
        {
            int size = dimensionSizes.AggregateOrDefault((x, y) => x * y);
            var rangeOfIndices = new InclusiveValueRange<int>(size - 1, 0);
            if (!rangeOfIndices.Contains(index))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var coordinates = new int[dimensionSizes.Length];

            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = index % dimensionSizes[i];
                index /= dimensionSizes[i];
            }

            return coordinates;
        }
    }
}
