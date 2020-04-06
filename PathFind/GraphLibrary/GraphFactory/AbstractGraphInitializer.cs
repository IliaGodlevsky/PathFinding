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
            int graphWidth = info.GetLength(0);
            int graphHeight = info.Length / info.GetLength(0);
            SetGraph(graphWidth, graphHeight);
            for (int i = 0; i < graphWidth; i++)
            {
                for (int j = 0; j < graphHeight; j++)
                {
                    vertices[i, j] = CreateVertex(info[i, j]);
                    vertices[i, j].Location = new Point(i * placeBetweenVertices, 
                        j * placeBetweenVertices);                    
                }
            }
        }

        public abstract IVertex CreateVertex( VertexInfo info);
        public abstract void SetGraph(int width, int height);
        public abstract AbstractGraph GetGraph();
       
    }
}
