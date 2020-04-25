using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using GraphLibrary.Graph;
using WinFormsVersion.Vertex;
using WinFormsVersion.Graph;

namespace WinFormsVersion.GraphFactory
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
                Text = (rand.Next(Const.MAX_VERTEX_VALUE) + Const.MIN_VERTEX_VALUE).ToString()
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
