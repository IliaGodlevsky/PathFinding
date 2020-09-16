using GraphLibrary.Graphs.Interface;
using System;

namespace GraphLibrary.GraphSerialization.GraphSaver.Interface
{
    public interface IGraphSaver
    {
        event Action<string> OnBadSave;
        void SaveGraph(IGraph graph, string path);
    }
}
