using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System.Windows;

namespace WPFVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex(INeighborhood coordinateRadar, ICoordinate coordinate)
        {
            return Application.Current.Dispatcher.Invoke(() => new Vertex(coordinateRadar, coordinate));
        }
    }
}
