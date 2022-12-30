using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.ValueInput.ProgrammedInput.FromFile
{
    internal sealed class FromFileProgrammedConsoleKeyInput : FromFileProgrammedInput<ConsoleKey>
    {
        public FromFileProgrammedConsoleKeyInput()
            : base("Script_ConsoleKey.txt")
        {
        }

        public override ConsoleKey Input()
        {
            var wait = TimeSpan.FromMilliseconds(20);
            var input = Steps.Dequeue();
            wait.Wait();
            return input;
        }

        protected override bool Parse(string value, out ConsoleKey output)
        {
            return Enum.TryParse(value, out output);
        }
    }
}
