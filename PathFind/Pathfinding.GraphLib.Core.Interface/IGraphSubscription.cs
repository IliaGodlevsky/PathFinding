namespace Pathfinding.GraphLib.Core.Interface
{
    public interface IGraphSubscription<in TVertex>
        where TVertex : IVertex
    {
        void Subscribe(IGraph<TVertex> graph);

        void Unsubscribe(IGraph<TVertex> graph);
    }
}
