using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class GraphDeletedMessage
    {
        public int Id { get; }

        public GraphDeletedMessage(int id)
        {
            Id = id;
        }
    }
}
