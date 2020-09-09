using GraphLibrary.Graphs;
using System;

namespace GraphLibrary.GraphSerialization.GraphSaver.Interface
{
    public interface IGraphSaver
    {
        event Action<string> OnBadSave;
        void SaveGraph(Graph graph, string path);
    }
}
