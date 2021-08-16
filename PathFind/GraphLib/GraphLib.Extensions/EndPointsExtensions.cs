using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    sealed class EndPoints : IEndPoints
    {
        public IVertex Target { get; }
        public IVertex Source { get; }

        public EndPoints(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
        }

        public EndPoints(IEndPoints endPoints)
            : this(endPoints.Source, endPoints.Target)
        {

        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Source)
                || vertex.IsEqual(Target);
        }
    }

    public static class EndPointsExtensions
    {
        public static IEnumerable<IEndPoints> ToIntermediateEndPoints(this IIntermediateEndPoints self)
        {
            var vertices = self.IntermediateVertices
                .Prepend(self.Source)
                .Append(self.Target)
                .ToArray();

            for (int i = 0; i < vertices.Length - 1; i++)
            {
                yield return new EndPoints(vertices[i], vertices[i + 1]);
            }
        }
    }
}
