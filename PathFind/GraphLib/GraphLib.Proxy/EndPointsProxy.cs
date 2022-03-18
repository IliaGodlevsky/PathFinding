using Common.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Proxy
{
    internal sealed class EndPointsProxy : IEndPoints
    {
        public IVertex Target { get; }
        public IVertex Source { get; }
        public IEnumerable<IVertex> EndPoints { get; }

        public EndPointsProxy(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
            EndPoints = (Source, Target).Merge();
        }
    }
}
