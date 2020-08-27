using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using GraphLibrary.Graph;
using WinFormsVersion.Vertex;
using WinFormsVersion.Graph;
using GraphLibrary.Extensions.RandomExtension;
using WinFormsVersion.StatusSetter;

namespace WinFormsVersion.GraphFactory
{
    public class RandomValuedWinFormsGraphFactory : AbstractGraphFactory
    {
        public RandomValuedWinFormsGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
                width, height, placeBetweenButtons)
        {
            
        }

        protected override IVertex CreateGraphTop() => new WinFormsVertex { Text = rand.GetRandomVertexValue() };

        public override AbstractGraph GetGraph() => new WinFormsGraph(vertices);

        protected override void SetGraph(int width, int height) => vertices = new WinFormsVertex[width, height];
    }
}
