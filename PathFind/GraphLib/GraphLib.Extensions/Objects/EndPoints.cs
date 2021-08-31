using GraphLib.Interfaces;

namespace GraphLib.Extensions.Objects
{
    internal sealed class EndPoints : IEndPoints
    {
        public IVertex Target { get; }
        public IVertex Source { get; }

        public EndPoints(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsOneOf(Source, Target);
        }
    }
}
