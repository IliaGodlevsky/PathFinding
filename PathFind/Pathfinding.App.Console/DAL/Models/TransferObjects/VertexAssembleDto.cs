using Pathfinding.GraphLib.Core.Realizations;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects
{
    internal class VertexAssembleDto
    {
        public int Id { get; set; }

        public bool IsObstacle { get; set; }

        public VertexCost Cost { get; set; }

        public Coordinate Coordinate { get; set; }
    }
}
