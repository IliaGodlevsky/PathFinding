namespace Pathfinding.App.Console.Messages
{
    internal sealed class GraphParametresMessage
    {
        public int Width { get; }

        public int Length { get; }

        public GraphParametresMessage(int width, int length)
        {
            Width = width;
            Length = length;
        }
    }
}