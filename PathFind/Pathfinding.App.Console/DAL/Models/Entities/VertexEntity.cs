namespace Pathfinding.App.Console.DAL.Models.Entities;

internal class VertexEntity
{
    public int Id { get; set; }

    public int GraphId { get; set; }

    public int Order { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public int Cost { get; set; }

    public int UpperValueRange { get; set; }

    public int LowerValueRange { get; set; }

    public bool IsObstacle { get; set; }
}
