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
        protected override AbstractGraph CreateGraph(AbstractGraphInitializer initializer) => initializer.GetGraph();

        protected override AbstractGraphInitializer GetInitializer(VertexInfo[,] info) => new ConsoleGraphInitializer(info);

        protected override string GetPath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }

        protected override void ShowMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
