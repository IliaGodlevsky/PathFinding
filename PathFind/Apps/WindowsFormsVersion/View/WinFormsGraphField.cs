using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
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
        private readonly int distanceBetweenVertices;

        public IReadOnlyCollection<IVertex> Vertices { get; }

        public WinFormsGraphField(Graph2D graph)
        {
            distanceBetweenVertices = Constants.DistanceBetweenVertices + Constants.VertexSize;
            Vertices = graph.Vertices;
            graph.ForEach<Vertex>(Locate);
        }

        public WinFormsGraphField()
        {

        }

        private void Locate(Vertex vertex)
        {
            var position = (Coordinate2D)vertex.Position;

            var xCoordinate = position.X * distanceBetweenVertices;
            var yCoordinate = position.Y * distanceBetweenVertices;

            vertex.Location = new Point(xCoordinate, yCoordinate);

            Controls.Add(vertex);
        }
    }
}
