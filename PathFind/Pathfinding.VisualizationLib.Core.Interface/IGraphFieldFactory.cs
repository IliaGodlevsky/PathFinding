using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IGraphFieldFactory<TVertex, TField>
        where TVertex : IVertex
        where TField : IGraphField<TVertex>
    {
        TField CreateGraphField(IGraph<TVertex> graph);
    }
}
