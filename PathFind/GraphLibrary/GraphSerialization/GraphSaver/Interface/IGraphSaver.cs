using GraphLibrary.Graphs.Interface;
using System;

namespace GraphLibrary.GraphSerialization.GraphSaver.Interface
{
    /// <summary>
    /// Presents methods to serialize graph
    /// </summary>
    public interface IGraphSaver
    {
        event Action<string> OnBadSave;
        void SaveGraph(IGraph graph, string path);
    }
}
