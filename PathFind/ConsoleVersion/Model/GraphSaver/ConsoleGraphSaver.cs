using GraphLibrary.GraphSaver;
using System;

namespace ConsoleVersion.GraphSaver
{
    internal class ConsoleGraphSaver : AbstractGraphSaver
    {
        protected override string GetPath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }

        protected override void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
