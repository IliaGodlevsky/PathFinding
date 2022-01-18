using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsVersion.Model;

namespace WindowsFormsVersion.View
{
    internal sealed class WinFormsGraphField : UserControl, IGraphField
    {
        public IReadOnlyCollection<IVertex> Vertices { get; }
        public WinFormsGraphField(Graph2D graph)
        {
            distanceBetweenVertices = Constants.DistanceBetweenVertices + Constants.VertexSize;
            Vertices = graph.Vertices;
            Vertices.ForEach(Add);
        }

        public WinFormsGraphField()
        {

        }

        private void Add(IVertex vertex)
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
