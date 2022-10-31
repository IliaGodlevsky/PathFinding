namespace GraphLib.Interfaces
{
    public interface IGraphFieldFactory<TGraph, TVertex, TField>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
        where TField : IGraphField<TVertex>
    {
        TField CreateGraphField(TGraph graph);
    }
}
