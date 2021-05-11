using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace Algorithm.Сompanions
{
    public readonly struct EndPoints : IEndPoints
    {
        public IVertex End { get; }
        public IVertex Start { get; }

        public EndPoints(IVertex start, IVertex end)
        {
            Start = start;
            End = end;
        }

        public EndPoints(IEndPoints endPoints)
        {
            End = endPoints.End;
            Start = endPoints.Start;
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Start)
                   || vertex.IsEqual(End)
                   || vertex.IsNullObject();
        }
    }
}