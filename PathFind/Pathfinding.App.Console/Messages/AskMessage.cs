namespace Pathfinding.App.Console.Messages
{
    internal class AskMessage<T>
        where T : class, new()
    {
        public T Response { get; set; } = new();
    }
}
