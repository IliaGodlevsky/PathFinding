using SearchAlgorithms.Top;

namespace SearchAlgorithms.Algorithms.IGraphFactory
{
    public class RandomValuedButtonGraphFactory : RandomButtonGraphFactory
    {
        public RandomValuedButtonGraphFactory(int percentOfObstacles, 
            int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
                width, height, placeBetweenButtons)
        {

        }

        public override void CreateGraphTop(ref IGraphTop button)
        {
            base.CreateGraphTop(ref button);
            (button as GraphTop).Text = (rand.Next(9) + 1).ToString();
        }
    }
}
