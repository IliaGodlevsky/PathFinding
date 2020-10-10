using GraphLibrary.Coordinates;
using GraphLibrary.GraphField;
using GraphLibrary.Vertex.Interface;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsVersion.Vertex;

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
                throw new ArgumentException("Must be 2D coordinates");
            int sizeBetween = GraphLibrary.Globals.VertexParametres.SizeBetweenVertices;
            if (sizeBetween < 0)
                sizeBetween = 26;
            (vertex as WinFormsVertex).Location
                = new Point(coordinate.X * sizeBetween, coordinate.Y * sizeBetween);
            Controls.Add(vertex as WinFormsVertex);
        }
    }
}
