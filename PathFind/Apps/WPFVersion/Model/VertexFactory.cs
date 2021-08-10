using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using System.Windows;

namespace WPFVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(INeighboursCoordinates coordinateRadar, ICoordinate coordinate)
        {
            IVertex vertex = new NullVertex();
            Application.Current.Dispatcher.Invoke(() => vertex = new Vertex(coordinateRadar, coordinate));
            return vertex;
        }
    }
}
