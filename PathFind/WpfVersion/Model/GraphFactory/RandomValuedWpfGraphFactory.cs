using GraphLibrary.Constants;
using GraphLibrary.Graph;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using WpfVersion.Model.Graph;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.GraphFactory
{
    public class RandomValuedWpfGraphFactory : AbstractGraphFactory
    {
        public RandomValuedWpfGraphFactory(int percentOfObstacles, int width, int height, int placeBetweenVertices) : 
            base(percentOfObstacles, width, height, placeBetweenVertices)
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
