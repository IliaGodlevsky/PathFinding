using SearchAlgorythms.Top;

namespace SearchAlgorythms.GraphFactory
{
    public class RandomValuedButtonGraphFactory : RandomButtonGraphFactory
    {
        public RandomValuedButtonGraphFactory(int percentOfObstacles, 
            int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
                width, height, placeBetweenButtons)
        {

        }

        public override void CreateGraphTop(ref GraphTop button)
        {
            base.CreateGraphTop(ref button);
            button.Text = (rand.Next(9) + 1).ToString();
        }
    }
}
