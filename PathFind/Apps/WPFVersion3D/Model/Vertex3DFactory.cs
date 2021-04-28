using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DFactory : IVertexFactory
    {
        public Vertex3DFactory(IModel3DFactory modelFactory)
        {
            model3Dfactory = modelFactory;
        }

        public IVertex CreateVertex(ICoordinateRadar coordinateRadar, ICoordinate coordinate)
        {
            return new Vertex3D(coordinateRadar, coordinate, model3Dfactory);
        }

        private readonly IModel3DFactory model3Dfactory;
    }
}
