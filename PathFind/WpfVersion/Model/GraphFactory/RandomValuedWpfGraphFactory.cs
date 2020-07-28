using GraphLibrary.Constants;
using GraphLibrary.Extensions.RandomExtension;
using GraphLibrary.Graph;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using WpfVersion.Model.Graph;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.GraphFactory
{
    public class RandomValuedWpfGraphFactory : AbstractGraphFactory
    {
        public RandomValuedWpfGraphFactory(int percentOfObstacles, 
            int width, int height, int placeBetweenVertices) : 
            base(percentOfObstacles, width, height, placeBetweenVertices)
        {
        }

        public override AbstractGraph GetGraph() => new WpfGraph(vertices);

        protected override IVertex CreateGraphTop() => new WpfVertex { Text = rand.GetRandomVertexValue() };

        protected override void SetGraph(int width, int height) => vertices = new WpfVertex[width, height];
    }
}
