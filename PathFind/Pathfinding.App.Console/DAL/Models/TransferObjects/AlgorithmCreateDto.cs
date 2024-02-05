using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class AlgorithmCreateDto
    {
        public int GraphId { get; set; }

        public Statistics Statistics { get; set; }

        public IReadOnlyCollection<ICoordinate> Path { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }

        public IReadOnlyCollection<(ICoordinate, IReadOnlyList<ICoordinate>)> Visited { get; set; }

        public IReadOnlyCollection<ICoordinate> Obstacles { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
