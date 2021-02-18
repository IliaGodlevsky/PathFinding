using GraphLib.Interface;
using GraphLib.Realizations;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsVersion.Model;

namespace WindowsFormsVersion.View
{
    internal class WinFormsGraphField : UserControl, IGraphField
    {
        public WinFormsGraphField()
        {
            distanceBetweenVertices = Constants.DistanceBetweenVertices + Constants.VertexSize;
        }

        public void Add(IVertex vertex)
        {
            if (vertex.Position is Coordinate2D coordinate)
            {
                var winFormsVertex = vertex as Vertex;

                var xCoordinate = coordinate.X * distanceBetweenVertices;
                var yCoordinate = coordinate.Y * distanceBetweenVertices;

                winFormsVertex.Location = new Point(xCoordinate, yCoordinate);

                Controls.Add(winFormsVertex);
            }
            else
            {
                throw new ArgumentException("Must be 2D coordinates");
            }
        }

        private readonly int distanceBetweenVertices;
    }
}
