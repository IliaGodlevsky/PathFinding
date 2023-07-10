using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Heuristics
{
    public sealed class ManhattanDistance : Distance
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override double Zip(int first, int second)
        {
            return Math.Abs(first - second);
        }

        public override string ToString()
        {
            return "Manhattan distance";
        }
    }
}