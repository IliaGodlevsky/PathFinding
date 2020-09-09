using GraphLibrary.Graphs;
using System;

namespace GraphLibrary.GraphSerialization.GraphLoader.Interface
{
    public interface IGraphLoader
    {
        event Action<string> OnBadLoad;
        Graph GetGraph(string path);
    }
}
