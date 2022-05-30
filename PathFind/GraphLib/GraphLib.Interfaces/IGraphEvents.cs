namespace GraphLib.Interfaces
{
    public interface IGraphEvents
    {
        void Subscribe(IGraph graph);

        void Unsubscribe(IGraph graph);
    }
}
