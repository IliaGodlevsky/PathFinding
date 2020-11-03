using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Model
{
    public class Wpf3dGraphField : ModelVisual3D, IGraphField
    {
        public void Add(IVertex vertex)
        {
            var size = (vertex as Wpf3dVertex).Size;
            var coordinates = vertex.Position.Coordinates;
            var translate = new TranslateTransform3D
            {
                OffsetX = coordinates.First() * size + coordinates.First() * 2,
                OffsetY = coordinates.ElementAt(1) * size + coordinates.ElementAt(1) * 2,
                OffsetZ = coordinates.Last() * size + coordinates.Last() * 2
            };
            (vertex as Wpf3dVertex).Transform = translate;
            Children.Add(vertex as Wpf3dVertex);
        }
    }
}
