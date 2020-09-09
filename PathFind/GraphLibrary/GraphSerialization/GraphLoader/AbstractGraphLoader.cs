using GraphLibrary.DTO;
using GraphLibrary.GraphFactory;
using GraphLibrary.Graphs;
using GraphLibrary.GraphSerialization.GraphLoader.Interface;
using GraphLibrary.VertexBinding;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSerialization.GraphLoader
{
    public abstract class AbstractGraphLoader : IGraphLoader
    {
        protected Graph graph = null;

        public event Action<string> OnBadLoad;

        public Graph GetGraph(string path)
        {
            var formatter = new BinaryFormatter();
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                    Initialise((VertexInfo[,])formatter.Deserialize(stream));
            }
            catch (Exception ex)
            {
                OnBadLoad?.Invoke(ex.Message);
            }
            return graph;
        }

        private void Initialise(VertexInfo[,] info)
        {
            if (info == null)
                return;
            graph = GetInitializer(info).Graph;
            VertexBinder.ConnectVertices(graph);
        }
        protected abstract AbstractGraphInfoInitializer GetInitializer(VertexInfo[,] info);
    }
}
