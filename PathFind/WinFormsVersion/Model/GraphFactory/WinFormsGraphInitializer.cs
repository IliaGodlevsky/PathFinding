using GraphLibrary.GraphFactory;
using WinFormsVersion.Vertex;
using GraphLibrary.DTO;
using GraphLibrary.Vertex.Interface;

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
