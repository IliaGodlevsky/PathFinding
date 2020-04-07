using SearchAlgorythms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphSaver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

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
                try
                {
                    using (var stream = new FileStream(GetPath(), FileMode.Create))
                        formatter.Serialize(stream, info);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public abstract string GetPath();
    }
}
