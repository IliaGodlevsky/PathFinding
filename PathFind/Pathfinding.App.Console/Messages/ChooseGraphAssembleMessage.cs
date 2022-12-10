using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class ChooseGraphAssembleMessage
    {
        public IGraphAssemble<Graph2D<Vertex>, Vertex> Assemble { get; }

        public ChooseGraphAssembleMessage(IGraphAssemble<Graph2D<Vertex>, Vertex> assemble)
        {
            Assemble = assemble;
        }
    }
}
