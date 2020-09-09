using GraphLibrary.GraphFactory;
using WinFormsVersion.Vertex;
using GraphLibrary.Vertex.Interface;

namespace WinFormsVersion.GraphFactory
{
    internal class WinFormsGraphFactory : AbstractGraphFactory
    {
        public WinFormsGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
                width, height, placeBetweenButtons)
        {
            
        }

        protected override IVertex CreateVertex() => new WinFormsVertex();
    }
}
