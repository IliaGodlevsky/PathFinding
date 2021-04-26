using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DFactory : IVertexFactory
    {
        public IVertex CreateVertex(ICoordinateRadar coordinateRadar, ICoordinate coordinate)
        {
            return new Vertex3D(coordinateRadar, coordinate);
        }
    }
}
