using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsVersion.Model;

namespace WindowsFormsVersion.View
{
    internal sealed class WinFormsGraphField : UserControl, IGraphField
    {
        public WinFormsGraphField()
        {
            distanceBetweenVertices = Constants.DistanceBetweenVertices + Constants.VertexSize;
        }

        public void Add(IVertex vertex)
        {
            if (vertex.Position is Coordinate2D coordinate && vertex is Vertex winFormsVertex)
            {
                var xCoordinate = coordinate.X * distanceBetweenVertices;
                var yCoordinate = coordinate.Y * distanceBetweenVertices;

                winFormsVertex.Location = new Point(xCoordinate, yCoordinate);

                Controls.Add(winFormsVertex);
            }
        }

        private readonly int distanceBetweenVertices;
    }
}
