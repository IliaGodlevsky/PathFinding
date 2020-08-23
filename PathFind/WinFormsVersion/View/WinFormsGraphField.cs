using GraphLibrary.Model;
using GraphLibrary.Vertex;
using System.Windows.Forms;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.Model
{
    public class WinFormsGraphField : UserControl, IGraphField
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
