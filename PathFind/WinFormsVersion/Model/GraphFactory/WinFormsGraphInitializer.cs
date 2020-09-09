using GraphLibrary.GraphFactory;
using WinFormsVersion.Vertex;
using GraphLibrary.DTO;
using GraphLibrary.Vertex.Interface;

namespace WinFormsVersion.GraphFactory
{
    internal class WinFormsGraphInitializer : AbstractGraphInfoInitializer
    {

        public WinFormsGraphInitializer(VertexDto[,] info, int placeBetweenTops)
            : base(info, placeBetweenTops)
        {
           
        }

        protected override IVertex CreateVertex(VertexDto info)
        {
            return new WinFormsVertex(info);
        }
    }
}
