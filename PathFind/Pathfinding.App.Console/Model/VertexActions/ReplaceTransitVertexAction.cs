using Pathfinding.App.Console.Interface;
using Pathfinding.Infrastructure.Business.Commands;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class ReplaceTransitVertexAction : IVertexAction
    {
        private readonly ReplaceTransitVerticesModule<Vertex> module;

        public ReplaceTransitVertexAction(ReplaceTransitVerticesModule<Vertex> module)
        {
            this.module = module;
        }

        public void Invoke(Vertex vertex)
        {
            module.ReplaceTransitWith(vertex);
        }
    }
}
