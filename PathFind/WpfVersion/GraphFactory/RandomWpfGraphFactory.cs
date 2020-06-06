using GraphLibrary.Constants;
using GraphLibrary.Graph;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using WpfVersion.Graph;
using WpfVersion.Vertex;

namespace WpfVersion.GraphFactory
{
    class RandomWpfGraphFactory : AbstractGraphFactory
    {
        public RandomWpfGraphFactory(int percentOfObstacles,
           int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
               width, height, placeBetweenButtons)
        {

        }
        public override AbstractGraph GetGraph()
        {
            return new WpfGraph(vertices);
        }

        protected override IVertex CreateGraphTop()
        {
            return new WpfVertex
            {
                Text = (rand.Next(Const.MAX_VERTEX_VALUE) + Const.MIN_VERTEX_VALUE).ToString()
            };
        }

        protected override void SetGraph(int width, int height)
        {
            vertices = new WpfVertex[width, height];
        }
    }
}
