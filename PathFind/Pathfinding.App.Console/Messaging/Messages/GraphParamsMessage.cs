namespace Pathfinding.App.Console.Messaging.Messages
{
    internal sealed class GraphParamsMessage
    {
        public int Width { get; }

        public int Length { get; }

        public GraphParamsMessage(int width, int length)
        {
            Width = width;
            Length = length;
        }
    }
}
