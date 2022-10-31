using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Drawing;
using WindowsFormsVersion.View;

namespace WindowsFormsVersion.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory<Graph2D<Vertex>, Vertex, WinFormsGraphField>
    {
        public WinFormsGraphField CreateGraphField(Graph2D<Vertex> graph)
        {
            return new WinFormsGraphField(graph) { Location = new Point(4, 90) };
        }
    }
}
