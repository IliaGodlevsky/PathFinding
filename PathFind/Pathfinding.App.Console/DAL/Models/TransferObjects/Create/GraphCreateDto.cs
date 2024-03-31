using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Create
{
    internal class GraphCreateDto<T>
        where T : IVertex
    {
        public string Name { get; set; }

        public IGraph<T> Graph { get; set; }
    }
}
