namespace Pathfinding.App.Console.Messages.DataMessages
{
    internal class DataMessage<T>
    {
        public T Value { get; }

        public DataMessage(T value)
        {
            Value = value;
        }
    }
}
