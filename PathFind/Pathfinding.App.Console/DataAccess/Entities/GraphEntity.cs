using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class GraphEntity : IEntity<int>
    {
        public int Id { get; set; }

        public IGraph<Vertex> Graph { get; set; }
    }
}
