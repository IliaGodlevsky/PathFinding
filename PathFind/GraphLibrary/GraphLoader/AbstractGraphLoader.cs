using GraphLibrary.GraphFactory;
using SearchAlgorythms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphLoader;
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
                {
                    VertexInfo[,] vertexInfo = (VertexInfo[,])formatter.Deserialize(stream);
                    Initialise(vertexInfo);
                }
            }
            catch (Exception ex)
            {
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

        public abstract void ShowMessage(string message);
        public abstract AbstractGraphInitializer GetInitializer(VertexInfo[,] info);
        public abstract AbstractGraph CreateGraph(AbstractGraphInitializer initializer);
        public abstract string GetPath();
    }
}
