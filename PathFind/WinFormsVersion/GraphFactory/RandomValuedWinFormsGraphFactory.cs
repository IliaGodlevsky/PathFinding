using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.GraphFactory
{
    public class RandomValuedWinFormsGraphFactory : AbstractGraphFactory
    {
        public RandomValuedWinFormsGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
                width, height, placeBetweenButtons)
        {
            
        }

        protected override IVertex CreateGraphTop()
        {
            return new WinFormsVertex
            {
                Text = (rand.Next(9) + 1).ToString()
            };
        }

        public override AbstractGraph GetGraph()
        {
            return new WinFormsGraph(vertices);
        }

        protected override void SetGraph(int width, int height)
        {
            vertices = new WinFormsVertex[width, height];
        }
    }
}
