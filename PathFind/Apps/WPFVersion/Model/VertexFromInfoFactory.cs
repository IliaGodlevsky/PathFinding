using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using System.Windows;

namespace WPFVersion.Model
{
    internal sealed class VertexFromInfoFactory : IVertexFromInfoFactory
    {
        public IVertex CreateFrom(VertexSerializationInfo info)
        {
            IVertex vertex = new NullVertex();
            Application.Current.Dispatcher.Invoke(() => vertex = new Vertex(info));
            return vertex;
        }
    }
}
