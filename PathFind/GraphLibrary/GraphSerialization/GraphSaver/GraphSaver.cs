using GraphLibrary.Graphs.Interface;
using GraphLibrary.GraphSerialization.GraphSaver.Interface;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSerialization.GraphSaver
{
    /// <summary>
    /// Saves graph using BinaryFormatter class
    /// </summary>
    public class GraphSaver : IGraphSaver
    {
        public event Action<string> OnBadSave;

        public void SaveGraph(IGraph graph, Stream stream)
        {
            var formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, graph.VertexDtos);
            }
            catch (Exception ex)
            {
                OnBadSave?.Invoke(ex.Message);
            }
        }
    }
}
