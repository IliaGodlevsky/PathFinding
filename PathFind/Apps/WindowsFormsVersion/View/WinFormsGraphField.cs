using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsVersion.Model;

namespace WindowsFormsVersion.View
{
    internal sealed class WinFormsGraphField : UserControl, IGraphField
    {
        public IReadOnlyCollection<IVertex> Vertices => Controls.OfType<IVertex>().ToArray();

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

        public void Clear()
        {
            Controls.Clear();
        }

        private readonly int distanceBetweenVertices;
    }
}
