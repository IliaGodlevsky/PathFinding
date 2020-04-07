using ConsoleVersion.GraphFactory;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphLoader;
using SearchAlgorythms;
using SearchAlgorythms.Graph;
using System;

namespace ConsoleVersion.GraphLoader
{
    public class ConsoleGraphLoader : AbstractGraphLoader
    {
        public override AbstractGraph CreateGraph(AbstractGraphInitializer initializer) => initializer.GetGraph();

        public override AbstractGraphInitializer GetInitializer(VertexInfo[,] info) => new ConsoleGraphInitializer(info);

        public override string GetPath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }

        public override void ShowMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
