using ConsoleVersion.Model.GraphFactory;
using GraphLibrary.DTO;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphSerialization.GraphLoader;
using System;

namespace ConsoleVersion.Model.GraphLoader
{
    internal class ConsoleGraphLoader : AbstractGraphLoader
    {
        protected override AbstractGraphInfoInitializer GetInitializer(VertexInfo[,] info) => new ConsoleGraphInitializer(info);

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
