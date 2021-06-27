using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NullObject.Attributes;

namespace Algorithm.Realizations.Heuristic
{
    [Null]
    public sealed class NullHeuristic : IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            return default;
        }
    }
}
