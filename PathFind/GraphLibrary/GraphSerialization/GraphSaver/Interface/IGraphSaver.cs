using GraphLibrary.Graphs.Interface;
using System;
using System.IO;

namespace GraphLibrary.GraphSerialization.GraphSaver.Interface
{
    /// <summary>
    /// Presents methods to serialize graph
    /// </summary>
    public interface IGraphSaver
    {
        event Action<string> OnBadSave;
        void SaveGraph(IGraph graph, Stream stream);
    }
}
