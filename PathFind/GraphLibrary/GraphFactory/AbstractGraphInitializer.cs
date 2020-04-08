using GraphLibrary.Extensions.MatrixExtension;
using SearchAlgorythms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System.Drawing;

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

        public abstract IVertex CreateVertex(VertexInfo info);
        public abstract void SetGraph(int width, int height);
        public abstract AbstractGraph GetGraph();
       
    }
}
