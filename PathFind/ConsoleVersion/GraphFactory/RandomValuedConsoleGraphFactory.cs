using GraphLibrary.Constants;
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

        protected override IVertex CreateGraphTop() => new ConsoleVertex { Text = (rand.Next(Const.MAX_VERTEX_VALUE) + Const.MIN_VERTEX_VALUE).ToString() };

        public override AbstractGraph GetGraph() => new ConsoleGraph(vertices);

        protected override void SetGraph(int width, int height) => vertices = new ConsoleVertex[width, height];
    }
}
