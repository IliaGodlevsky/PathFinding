using GraphLibrary;
using GraphLibrary.Graph;
using GraphLibrary.GraphFactory;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVersion.Model.GraphFactory
{
    public class WpfGraphInitializer : AbstractGraphInitializer
    {
        public WpfGraphInitializer(VertexInfo[,] info, int placeBetweenVertices) : base(info, placeBetweenVertices)
        {
        }

        public override AbstractGraph GetGraph()
        {
            throw new NotImplementedException();
        }

        protected override IVertex CreateVertex(VertexInfo info)
        {
            throw new NotImplementedException();
        }

        protected override void SetGraph(int width, int height)
        {
            throw new NotImplementedException();
        }
    }
}
