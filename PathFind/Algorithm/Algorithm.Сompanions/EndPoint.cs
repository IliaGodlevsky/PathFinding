using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace Algorithm.Сompanions
{
    public readonly struct EndPoints : IEndPoints
    {
        public IVertex Target { get; }
        public IVertex Source { get; }

        public EndPoints(IVertex start, IVertex end)
        {
            Source = start;
            Target = end;
        }

        public EndPoints(IEndPoints endPoints)
        {
            Target = endPoints.Target;
            Source = endPoints.Source;
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Source)
                   || vertex.IsEqual(Target)
                   || vertex.IsNullObject();
        }
    }
}