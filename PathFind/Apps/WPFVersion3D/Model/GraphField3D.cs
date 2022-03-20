using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;
using WPFVersion3D.Model.Axes;

using Axis = WPFVersion3D.Enums.Axis;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField
    {
        private readonly IDictionary<Axis, IAxis> axes;

        public IReadOnlyCollection<Vertex3D> Vertices { get; }

        IReadOnlyCollection<IVertex> IGraphField.Vertices => Vertices;

        public GraphField3D(Graph3D graph)
        {
            axes = new Dictionary<Axis, IAxis>();
            axes.Add(Axis.Applicate, new Applicate(graph));
            axes.Add(Axis.Ordinate, new Ordinate(graph));
            axes.Add(Axis.Abscissa, new Abscissa(graph));
            Vertices = graph.Vertices.OfType<Vertex3D>().ToArray();
            Children.AddRange(Vertices);
        }

        public void StretchAlongAxis(Axis axis, double distanceBetween)
        {
            axes[axis].Locate(this, distanceBetween);
        }
    }
}