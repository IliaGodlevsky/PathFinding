using SearchAlgorythms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphSaver;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleVersion.GraphSaver
{
    public class ConsoleGraphSaver : IGraphSaver
    {
        public void SaveGraph(AbstractGraph graph)
        {
            if (graph != null)
            {               
                VertexInfo[,] info = graph.Info;
                BinaryFormatter f = new BinaryFormatter();
                Console.Write("Enter path: ");
                string path = Console.ReadLine();
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                        f.Serialize(stream, info);                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
