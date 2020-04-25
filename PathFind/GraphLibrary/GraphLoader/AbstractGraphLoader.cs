using GraphLibrary.Graph;
using GraphLibrary.GraphFactory;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphLoader
{
    public abstract class AbstractGraphLoader : IGraphLoader
    {
        protected AbstractGraph graph = null;

        public AbstractGraph GetGraph()
        {
            IFormatter formatter = new BinaryFormatter();
            try {
                using (var stream = new FileStream(GetPath(), FileMode.Open))
                    Initialise((VertexInfo[,])formatter.Deserialize(stream));
            }
            catch (Exception ex) {
                ShowMessage(ex.Message);
            }
            return graph;
        }

        private void Initialise(VertexInfo[,] info)
        {
            if (info == null)
                return;
            graph = CreateGraph(GetInitializer(info));
            var setter = new NeigbourSetter(graph.GetArray());
            setter.SetNeighbours();
        }

        protected abstract void ShowMessage(string message);
        protected abstract AbstractGraphInitializer GetInitializer(VertexInfo[,] info);
        protected abstract AbstractGraph CreateGraph(AbstractGraphInitializer initializer);
        protected abstract string GetPath();
    }
}
