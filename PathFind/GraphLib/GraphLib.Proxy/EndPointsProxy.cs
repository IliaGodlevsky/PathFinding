using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace GraphLib.Proxy
{
    internal sealed class EndPointsProxy : IEndPoints
    {
        public IVertex Target { get; }

        public IVertex Source { get; }

        public EndPointsProxy(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            yield return Source;
            yield return Target;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
