namespace GraphLib.Interfaces
{
    public interface IGraphEvents<in TVertex>
        where TVertex : IVertex
    {
        void Subscribe(IGraph<TVertex> graph);

        void Unsubscribe(IGraph<TVertex> graph);
    }
}
