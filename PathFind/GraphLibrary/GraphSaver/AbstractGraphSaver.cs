using GraphFactory.GraphSaver;
using GraphLibrary.Graph;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSaver
{
    public abstract class AbstractGraphSaver : IGraphSaver
    {
        public void SaveGraph(AbstractGraph graph)
        {
            if (graph != null)
            {
                VertexInfo[,] info = graph.Info;
                IFormatter formatter = new BinaryFormatter();
                try {
                    using (var stream = new FileStream(GetPath(), FileMode.Create))
                        formatter.Serialize(stream, info);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected abstract string GetPath();
    }
}
