using GraphLibrary.GraphField;
using GraphLibrary.Vertex.Interface;
using System.Windows.Forms;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.Model
{
    internal class WinFormsGraphField : UserControl, IGraphField
    {
        public WinFormsGraphField()
        {
            BorderStyle = BorderStyle.FixedSingle;
        }

        public void Add(IVertex vertex, int xCoordinate, int yCoordinate)
        {
            Controls.Add(vertex as WinFormsVertex);
        }
    }
}
