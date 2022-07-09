using Common.Extensions;
using ConsoleVersion.Interface;
using System;
using System.Collections.Generic;

namespace ConsoleVersion.ValueInput.ProgrammedInput
{
    internal abstract class ProgrammedInput<T> : IInput<T>
    {
        protected static readonly TimeSpan Wait = TimeSpan.FromMilliseconds(500);

        private readonly Lazy<Queue<T>> steps;

        protected Queue<T> Steps => steps.Value;

        protected ProgrammedInput()
        {
            steps = new Lazy<Queue<T>>(GenerateCommands);
        }

        public virtual T Input()
        {
            var input = Steps.Dequeue();
            Console.WriteLine(input);
            Wait.Wait();
            return input;
        }

        protected abstract Queue<T> GenerateCommands();
    }
}