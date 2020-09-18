using GraphLibrary.Graphs.Interface;
using GraphLibrary.GraphSerialization.GraphSaver.Interface;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSerialization.GraphSaver
{
    /// <summary>
    /// Saves graph using BinaryFormatter class
    /// </summary>
    public class GraphSaver : IGraphSaver
    {
        public event Action<string> OnBadSave;

        public void SaveGraph(IGraph graph, string path)
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
