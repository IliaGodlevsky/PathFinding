using GraphLibrary.Graphs;
using GraphLibrary.GraphSerialization.GraphSaver.Interface;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSerialization.GraphSaver
{
    public class GraphSaver : IGraphSaver
    {
        public event Action<string> OnBadSave;

        public void SaveGraph(Graph graph, string path)
        {
            if (graph != null)
            {
                var formatter = new BinaryFormatter();
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                        formatter.Serialize(stream, graph.VerticesDto);
                }
                catch (Exception ex)
                {
                    OnBadSave?.Invoke(ex.Message);
                }
            }
        }
    }
}
