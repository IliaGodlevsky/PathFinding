using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class GraphJsonSerializationDto
    {
        public IReadOnlyList<int> DimensionSizes { get; set; }

        public IReadOnlyCollection<VertexJsonSerializationDto> Vertices { get; set; }
    }
}
