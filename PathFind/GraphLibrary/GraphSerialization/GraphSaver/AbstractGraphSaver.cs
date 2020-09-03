using GraphFactory.GraphSaver;
using GraphLibrary.Collection;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSaver
{
    public abstract class AbstractGraphSaver : IGraphSaver
    {
        public void SaveGraph(Graph graph)
        {
            if (graph != null)
            {
                var formatter = new BinaryFormatter();
                try {
                    using (var stream = new FileStream(GetPath(), FileMode.Create))
                        formatter.Serialize(stream, graph.VerticesInfo);
                }
                catch (Exception ex) {
                    ShowMessage(ex.Message);
                }
            }
        }

        protected abstract void ShowMessage(string message);
        protected abstract string GetPath();
    }
}
