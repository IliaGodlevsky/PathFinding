using Pathfinding.App.Console.DataAccess.ReadDto;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class GetAllGraphsMessage
    {
        public IReadOnlyCollection<GraphReadDto> Graphs { get; set; }
    }
}
