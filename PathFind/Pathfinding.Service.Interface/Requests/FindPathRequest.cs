using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests
{
    public class FindPathRequest
    {
        public IEnumerable<IVertex> Range { get; set; }

        public string Algorithm { get; set; }

        public string Heuristic { get; set; }

        public string StepRule { get; set; }
    }
}
