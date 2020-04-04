using ConsoleVersion.GraphFactory;
using SearchAlgorythms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphLoader;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleVersion.GraphLoader
{
    public class ConsoleGraphLoader : IGraphLoader
    {
        private AbstractGraph graph = null;

        public AbstractGraph GetGraph()
        {
            BinaryFormatter f = new BinaryFormatter();
            Console.Write("Enter path: ");
            string path = Console.ReadLine();
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    GraphTopInfo[,] info = (GraphTopInfo[,])f.Deserialize(stream);
                    Initialise(info);                   
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return graph;
        }

        private void Initialise(GraphTopInfo[,] info)
        {
            OnInfoConsoleGraphFactory creator =
                new OnInfoConsoleGraphFactory(info);
            if (info == null)
                return;
            graph = new ConsoleGraph(creator.GetGraph());
            NeigbourSetter setter = new NeigbourSetter(graph.GetArray());
            setter.SetNeighbours();
        }
    }
}
