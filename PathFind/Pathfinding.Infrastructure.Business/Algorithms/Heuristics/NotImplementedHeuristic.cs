using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using System;

namespace Pathfinding.Infrastructure.Business.Algorithms.Heuristics
{
    public sealed class NotImplementedHeuristic : IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            throw new NotImplementedException();
        }
    }
}
