using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ValueInput.ProgrammedInput
{
    internal sealed class ConsoleProgrammedKeyInput : ProgrammedInput<ConsoleKey>
    {
        private List<ConsoleKey> Keys { get; }

        public ConsoleProgrammedKeyInput()
        {
            Keys = new List<ConsoleKey>();
            Keys.AddRange(Enumerable.Repeat(ConsoleKey.UpArrow, 200));
        }

        public override ConsoleKey Input()
        {
            if (Steps.Count == 0)
            {
                Keys.ForEach(Steps.Enqueue);
            }
            Wait.Wait();
            return Steps.Dequeue();
        }

        protected override Queue<ConsoleKey> GenerateCommands()
        {
            return new Queue<ConsoleKey>(Keys);
        }
    }
}
