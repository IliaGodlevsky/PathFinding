using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Modules;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class ReplaceTransitVertexAction : IVertexAction
    {
        private readonly ReplaceTransitVerticesModule<Vertex> module;

        public ReplaceTransitVertexAction(ReplaceTransitVerticesModule<Vertex> module)
        {
            this.module = module;
        }

        public void Do(Vertex vertex)
        {
            module.ReplaceTransitWith(vertex);
        }
    }
}
