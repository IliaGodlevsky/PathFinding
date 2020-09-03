using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using WinFormsVersion.Vertex;
using GraphLibrary.Common.Constants;

namespace WinFormsVersion.GraphFactory
{
    internal class WinFormsGraphFactory : AbstractGraphFactory
    {
        public WinFormsGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenButtons) : base(percentOfObstacles,
                width, height, placeBetweenButtons)
        {
            
        }

        protected override IVertex CreateVertex() => new WinFormsVertex { Cost = VertexValueRange.GetRandomVertexValue() };
    }
}
