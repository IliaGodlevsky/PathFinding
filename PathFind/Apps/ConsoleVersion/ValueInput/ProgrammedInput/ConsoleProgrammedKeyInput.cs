using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.ValueInput.ProgrammedInput
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
            TimeSpan.FromMilliseconds(750).Wait();
            return Steps.Dequeue();
        }

        protected override Queue<ConsoleKey> GenerateCommands()
        {
            return new Queue<ConsoleKey>(Keys);
        }
    }
}
