using Pathfinding.App.Console.DataAccess.ReadDto;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class GetAlgorithmsMessage
    {
        public int GraphId { get; }

        public IReadOnlyCollection<AlgorithmReadDto> Algorithms { get; set; }

        public GetAlgorithmsMessage(int graphId)
        {
            GraphId = graphId;
        }
    }
}
