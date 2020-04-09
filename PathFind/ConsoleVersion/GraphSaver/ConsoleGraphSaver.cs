using GraphLibrary.GraphSaver;
using SearchAlgorythms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphSaver;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleVersion.GraphSaver
{
    public class ConsoleGraphSaver : AbstractGraphSaver
    {
        protected override string GetPath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }
    }
}
