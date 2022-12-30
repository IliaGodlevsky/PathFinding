using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface ILayer<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        void Overlay(TGraph graph);
    }
}
