using SearchAlgorythms.Top;

namespace SearchAlgorythms.GraphFactory
{
    public class RandomValuedButtonGraphFactory : AbstractGraphFactory
    {
        public RandomValuedButtonGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
                width, height, placeBetweenButtons)
        {
            
        }

        public override IGraphTop CreateGraphTop()
        {
            return new GraphTop
            {
                Text = (rand.Next(9) + 1).ToString()
            };
        }

        public override void SetGraph(int width, int height)
        {
            graph = new GraphTop[width, height];
        }
    }
}
