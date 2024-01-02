namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class GraphDeletedMessage
    {
        public int Id { get; }

        public GraphDeletedMessage(int id)
        {
            Id = id;
        }
    }
}
