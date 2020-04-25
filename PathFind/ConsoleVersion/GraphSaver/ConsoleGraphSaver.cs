using GraphLibrary.GraphSaver;
using System;

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
