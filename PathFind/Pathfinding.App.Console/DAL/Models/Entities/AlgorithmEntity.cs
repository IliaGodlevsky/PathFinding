namespace Pathfinding.App.Console.DAL.Models.Entities;

internal class AlgorithmEntity
{
    public int Id { get; set; }

    public int GraphId { get; set; }

    public string Statistics { get; set; }

    public byte[] Costs { get; set; }

    public byte[] Path { get; set; }

    public byte[] Obstacles { get; set; }

    public byte[] Visited { get; set; }

    public byte[] Range { get; set; }
}
