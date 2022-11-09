using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IGraphFieldFactory<TGraph, TVertex, TField>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
        where TField : IGraphField<TVertex>
    {
        TField CreateGraphField(TGraph graph);
    }
}
