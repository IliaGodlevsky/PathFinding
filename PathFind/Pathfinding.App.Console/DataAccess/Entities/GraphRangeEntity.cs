using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class GraphRangeEntity : IEntity<int>
    {
        public int Id { get; set; }

        public int AlgorithmId { get; set; }

        public IList<ICoordinate> Range { get; set; }
    }
}
