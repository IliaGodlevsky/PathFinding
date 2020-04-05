using SearchAlgorythms.Top;

namespace SearchAlgorythms.GraphFactory
{
    public class RandomValuedConsoleGraphFactory : AbstractGraphFactory
    {
        public RandomValuedConsoleGraphFactory(int percentOfObstacles,
        int width, int height) : base(percentOfObstacles, width, height, 1)
        {

        }

        public override IGraphTop CreateGraphTop()
        {
            return new ConsoleGraphTop
            {
                Text = (rand.Next(9) + 1).ToString()
            };
        }

        public override void SetGraph(int width, int height)
        {
            graph = new ConsoleGraphTop[width, height];
        }
    }
}
