using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Read
{
    internal class GraphInformationReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyList<int> Dimensions { get; set; }
        
        public int ObstaclesCount { get; set; }
    }
}
