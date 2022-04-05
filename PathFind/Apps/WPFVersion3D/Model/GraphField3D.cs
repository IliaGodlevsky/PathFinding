using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;
using WPFVersion3D.Model.Axes;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField
    {
        public IAxis Abscissa { get; }

        public IAxis Ordinate { get; }

        public IAxis Applicate { get; }

        private IReadOnlyCollection<Vertex3D> Vertices { get; }

        IReadOnlyCollection<IVertex> IGraphField.Vertices => Vertices;

        public GraphField3D(Graph3D graph)
        {
            Vertices = graph.Vertices.OfType<Vertex3D>().ToArray();
            Children.AddRange(Vertices);
            Abscissa = new Abscissa(graph.DimensionsSizes, Vertices);
            Ordinate = new Ordinate(graph.DimensionsSizes, Vertices);
            Applicate = new Applicate(graph.DimensionsSizes, Vertices);
        }
    }
}