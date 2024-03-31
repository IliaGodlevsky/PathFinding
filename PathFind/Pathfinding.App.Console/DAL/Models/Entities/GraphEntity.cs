namespace Pathfinding.App.Console.DAL.Models.Entities;

internal record GraphEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Dimensions { get; set; }

    public int ObstaclesCount { get; set; }
}
