using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Heuristics
{
    public abstract class Distance : IHeuristic
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(IVertex first, IVertex second)
        {
            double result = first.Position
                .Zip(second.Position, Zip)
                .Aggregate(Aggregate);
            return ProcessResult(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual double ProcessResult(double result) => result;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual double Aggregate(double a, double b) => a + b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract double Zip(int first, int second);
    }
}