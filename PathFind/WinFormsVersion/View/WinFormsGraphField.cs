using GraphLibrary.GraphField;
using GraphLibrary.Vertex.Interface;
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

        public void Add(IVertex vertex, int xCoordinate, int yCoordinate)
        {
            int sizeBetween = GraphLibrary.Globals.VertexSize.SIZE_BETWEEN_VERTICES;
            if (sizeBetween < 0)
                sizeBetween = 26;
            (vertex as WinFormsVertex).Location = new Point(xCoordinate * sizeBetween, yCoordinate * sizeBetween);
            Controls.Add(vertex as WinFormsVertex);
        }
    }
}
