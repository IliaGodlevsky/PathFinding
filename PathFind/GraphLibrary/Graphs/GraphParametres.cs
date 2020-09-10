namespace GraphLibrary.Graphs
{
    public struct GraphParametres
    {
        public int Width { get; }
        public int Height { get; }
        public int ObstaclePercent { get; }

        public GraphParametres(int width, int height, int obstaclePercent)
        {
            Width = width;
            Height = height;
            ObstaclePercent = obstaclePercent;
        }
    }
}
