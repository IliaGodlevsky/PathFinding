﻿using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Modules;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class MarkTransitToReplaceAction : IVertexAction
    {
        private readonly ReplaceTransitVerticesModule<Vertex> module;

        public MarkTransitToReplaceAction(ReplaceTransitVerticesModule<Vertex> module)
        {
            this.module = module;
        }

        public void Invoke(Vertex vertex)
        {
            module.MarkTransitVertex(vertex);
        }
    }
}
