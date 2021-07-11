using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace Algorithm.Сompanions
{
    public sealed class EndPoints : IEndPoints
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
                   || vertex.IsEqual(Target)
                   || vertex.IsNull();
        }
    }
}