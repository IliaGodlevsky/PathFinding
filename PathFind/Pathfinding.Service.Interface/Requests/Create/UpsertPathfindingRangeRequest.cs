using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Requests.Create
{
    public class UpsertPathfindingRangeRequest
    {
        public int GraphId { get; set; }

        public List<(int Id, bool IsSource, bool IsTarget, int VertexId, int Order)> Ranges { get; set; } = new();
    }
}
