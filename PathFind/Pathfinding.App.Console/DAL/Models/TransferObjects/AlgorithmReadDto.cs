using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class AlgorithmReadDto
    {
        public int Id { get; set; }

        public int GraphId { get; set; }

        public Statistics Statistics { get; set; }

        public IReadOnlyCollection<SubAlgorithmReadDto> SubAlgorithms { get; set; }

        public IReadOnlyCollection<ICoordinate> Range { get; set; }

        public IReadOnlyCollection<ICoordinate> Obstacles { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
