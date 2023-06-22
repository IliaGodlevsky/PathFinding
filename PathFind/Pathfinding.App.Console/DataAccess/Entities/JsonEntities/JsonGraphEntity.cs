using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Entities.JsonEntities
{
    public sealed class JsonGraphEntity : IIdentityItem<long>
    {
        public long Id { get; set; }

        public GraphJsonCoordinates[] Range { get; set; }

        public int[] Dimensions { get; set; }

        public bool[] Statuses { get; set; }

        public int[] Costs { get; set; }

        public GraphJsonCoordinates[] Coordinates { get; set; }

        public JsonNeighbours[] Neighbours { get; set; }
    }

    public sealed class GraphJsonCoordinates
    {
        public GraphJsonCoordinates() { }

        public int[] Coordinates { get; set; }
    }

    public sealed class JsonRange
    {

        public JsonRange() { }

        public List<GraphJsonCoordinates> Range { get; set; }
    }

    public sealed class JsonNeighbours
    {
        public JsonNeighbours() { }

        public GraphJsonCoordinates[] Neighbors { get; set; }
    }
}
