using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Vertex;
using System.Linq;

namespace GraphLibrary
{
    public class NeigbourSetter
    {
        private readonly IVertex[,] vertices;
        private readonly int width;
        private readonly int height;

        public NeigbourSetter(IVertex[,] vertices)
        {
            this.vertices = vertices;
            width = vertices.Width();
            height = vertices.Height();
        }

        public void SetNeighbours(int xCoordinate, int yCoordinate)
        {
            if (vertices[xCoordinate, yCoordinate].IsObstacle)
                return;
            for (int i = xCoordinate - 1; i <= xCoordinate + 1; i++)
            {
                for (int j = yCoordinate - 1; j <= yCoordinate + 1; j++)
                {
                    if (i >= 0 && i < width && j >= 0 && j < height) 
                    {
                        if (!vertices[i, j].IsObstacle)
                            vertices[xCoordinate, yCoordinate].Neighbours.Add(vertices[i, j]);
                    }
                }
            }
            vertices[xCoordinate, yCoordinate].Neighbours.
                Remove(vertices[xCoordinate, yCoordinate]);
        }

        public void SetNeighbours()
        {
            vertices.Cast<IVertex>().ToList().ForEach(vertex => 
            SetNeighbours(vertices.GetIndices(vertex).X, vertices.GetIndices(vertex).Y));
        }
    }
}
