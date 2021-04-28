using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DSerializationInfoConverter : IVertexSerializationInfoConverter
    {
        public Vertex3DSerializationInfoConverter(ICoordinateRadarFactory factory, IModel3DFactory model3DFactory)
        {
            this.factory = factory;
            this.model3DFactory = model3DFactory;
        }

        public IVertex ConvertFrom(VertexSerializationInfo info)
        {
            var radar = factory.CreateCoordinateRadar(info.Position);
            return new Vertex3D(info, radar, model3DFactory);
        }

        private readonly ICoordinateRadarFactory factory;
        private readonly IModel3DFactory model3DFactory;
    }
}
