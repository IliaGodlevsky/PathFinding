using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.GraphFactory
{
    public class RandomValuedConsoleGraphFactory : AbstractGraphFactory
    {
        public RandomValuedConsoleGraphFactory(int percentOfObstacles,
        int width, int height) : base(percentOfObstacles, width, height, 1)
        {

        }

        public override IVertex CreateGraphTop()
        {
            return new ConsoleVertex
            {
                Text = (rand.Next(9) + 1).ToString()
            };
        }

        public override AbstractGraph GetGraph()
        {
            return new ConsoleGraph(vertices);
        }

        public override void SetGraph(int width, int height)
        {
            vertices = new ConsoleVertex[width, height];
        }
    }
}
