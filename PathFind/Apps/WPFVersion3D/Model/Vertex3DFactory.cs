using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using System.Windows;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DFactory : IVertexFactory
    {
        public Vertex3DFactory(IModel3DFactory modelFactory)
        {
            model3Dfactory = modelFactory;
        }

        public IVertex CreateVertex(INeighboursCoordinates coordinateRadar, ICoordinate coordinate)
        {
            IVertex vertex = new NullVertex();
            Application.Current.Dispatcher.Invoke(() => vertex = new Vertex3D(coordinateRadar, coordinate, model3Dfactory));
            return vertex;
        }

        private readonly IModel3DFactory model3Dfactory;
    }
}
