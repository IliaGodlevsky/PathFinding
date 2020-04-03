using SearchAlgorythms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphSaver;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp1.GraphSaver
{
    public class ConsoleGraphSaver : IGraphSaver
    {
        public void SaveGraph(IGraph graph)
        {
            if (graph != null)
            {               
                GraphTopInfo[,] info = graph.GetInfo();
                BinaryFormatter f = new BinaryFormatter();
                string path = "Test";
                using (var stream = new FileStream(path, FileMode.Create)) 
                {
                    try
                    {
                        f.Serialize(stream, info);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }
            }
        }
    }
}
