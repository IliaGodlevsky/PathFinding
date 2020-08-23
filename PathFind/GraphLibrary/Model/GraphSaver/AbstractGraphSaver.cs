using GraphFactory.GraphSaver;
using GraphLibrary.Graph;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSaver
{
    public abstract class AbstractGraphSaver : IGraphSaver
    {
        public void SaveGraph(AbstractGraph graph)
        {
            if (graph != null)
            {
                var info = graph.Info;
                var formatter = new BinaryFormatter();
                try {
                    using (var stream = new FileStream(GetPath(), FileMode.Create))
                        formatter.Serialize(stream, info);
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
