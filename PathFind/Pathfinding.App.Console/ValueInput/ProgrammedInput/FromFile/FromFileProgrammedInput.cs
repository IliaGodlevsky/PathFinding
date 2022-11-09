using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pathfinding.App.Console.ValueInput.ProgrammedInput.FromFile
{
    internal abstract class FromFileProgrammedInput<T> : ProgrammedInput<T>
    {
        protected readonly string path;

        protected FromFileProgrammedInput(string path)
            : base()
        {
            this.path = path;
        }

        protected override Queue<T> GenerateCommands()
        {
            var commands = new List<T>();
            using (var stream = new StreamReader(path))
            {
                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    var splited = line.Split(':');
                    string value = splited.LastOrDefault();
                    if (!Parse(value, out var output))
                    {
                        throw new InvalidCastException();
                    }
                    commands.Add(output);
                }
            }
            return new Queue<T>(commands);
        }

        protected abstract bool Parse(string value, out T output);
    }
}
