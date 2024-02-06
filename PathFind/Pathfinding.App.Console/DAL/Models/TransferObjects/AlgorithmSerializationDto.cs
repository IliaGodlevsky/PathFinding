using Pathfinding.App.Console.Model.Notes;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class AlgorithmSerializationDto
    {
        public Statistics Statistics { get; set; }

        public IReadOnlyCollection<CoordinateDto> Range { get; set; }

        public IReadOnlyCollection<SubAlgorithmSerializationDto> SubAlgorithms { get; set; }

        public IReadOnlyCollection<CoordinateDto> Obstacles { get; set; }

        public IReadOnlyCollection<int> Costs { get; set; }
    }
}
