using GraphLibrary.GraphFactory;
using GraphLibrary;
using GraphLibrary.Vertex;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.GraphFactory
{
    internal class WinFormsGraphInitializer : AbstractGraphInfoInitializer
    {

        public WinFormsGraphInitializer(VertexInfo[,] info, int placeBetweenTops)
            : base(info, placeBetweenTops)
        {
           
        }

        protected override IVertex CreateVertex(VertexInfo info)
        {
            return new WinFormsVertex(info);
        }
    }
}
