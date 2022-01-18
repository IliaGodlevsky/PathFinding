using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Drawing;
using WindowsFormsVersion.View;

namespace WindowsFormsVersion.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory
    {
        public IGraphField CreateGraphField(IGraph graph)
        {
            return new WinFormsGraphField((Graph2D)graph) { Location = new Point(4, 90) };
        }
    }
}
