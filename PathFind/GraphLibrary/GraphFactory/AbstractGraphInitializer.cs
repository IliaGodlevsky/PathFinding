using GraphLibrary.Extensions.MatrixExtension;
using System.Drawing;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphInitializer : IGraphFactory
    {
        protected IVertex[,] vertices = null;

        public AbstractGraphInitializer(VertexInfo[,] info, int placeBetweenVertices)
        {
            if (info == null)
                return;
            SetGraph(info.Width(), info.Height());
            for (int i = 0; i < info.Width(); i++)
            {
                for (int j = 0; j < info.Height(); j++)
                {
                    vertices[i, j] = CreateVertex(info[i, j]);
                    vertices[i, j].Location = new Point(i * placeBetweenVertices, 
                        j * placeBetweenVertices);                    
                }
            }
        }

        protected abstract IVertex CreateVertex(VertexInfo info);
        protected abstract void SetGraph(int width, int height);
        public abstract AbstractGraph GetGraph();
       
    }
}
