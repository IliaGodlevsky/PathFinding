using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class GetGraphMessage
    {
        public int Id { get; }

        public IGraph<Vertex> Respond { get; set; }

        public GetGraphMessage(int id)
        {
            Id = id;
        }
    }
}
