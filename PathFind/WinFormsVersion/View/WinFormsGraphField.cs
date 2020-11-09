using Common;
using GraphLib.Coordinates;
using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsVersion.Model
{
    internal class WinFormsGraphField : UserControl, IGraphField
    {
        public WinFormsGraphField()
        {
            //BorderStyle = BorderStyle.FixedSingle;
        }

        public void Add(IVertex vertex)
        {
            var coordinate = vertex.Position as Coordinate2D;
            if (coordinate == null)
            {
                throw new ArgumentException("Must be 2D coordinates");
            }

            var winFormsVertex = vertex as WinFormsVertex;

            var xCoordinate = coordinate.X * VertexParametres.SizeBetweenVertices;
            var yCoordinate = coordinate.Y * VertexParametres.SizeBetweenVertices;

            winFormsVertex.Location = new Point(xCoordinate, yCoordinate);

            Controls.Add(vertex as WinFormsVertex);
        }
    }
}
