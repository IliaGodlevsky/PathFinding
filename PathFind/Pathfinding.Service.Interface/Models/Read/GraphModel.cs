using Pathfinding.Domain.Interface;


namespace Pathfinding.Service.Interface.Models.Read
{
    public record class GraphModel<T>
        where T : IVertex
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public IGraph<T> Graph { get; set; }
    }
}
