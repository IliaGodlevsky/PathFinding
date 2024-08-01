using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class PathfindingHistoriesModel<T>
        where T : IVertex
    {
        public List<PathfindingHistoryModel<T>> Models { get; set; }
            = new List<PathfindingHistoryModel<T>>();
    }
}
