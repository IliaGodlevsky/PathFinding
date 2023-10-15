using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface ILayer
    {
        void Overlay(IGraph<IVertex> graph);
    }
}
