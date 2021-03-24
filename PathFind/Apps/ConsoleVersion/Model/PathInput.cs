using GraphLib.Serialization.Interfaces;
using System;

namespace ConsoleVersion.Model
{
    internal class PathInput : IPathInput
    {
        public string InputLoadPath()
        {
            return InputPath();
        }

        public string InputSavePath()
        {
            return InputPath();
        }

        private string InputPath()
        {
            Console.Write(Message);
            return Console.ReadLine();
        }

        private const string Message = "Input path: ";
    }
}
