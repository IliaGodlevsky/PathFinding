using Common.Extensions;
using ConsoleVersion.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleVersion.ValueInput.ProgrammedInput
{
    internal abstract class ProgrammedInput<T> : IInput<T>
        where T : IComparable
    {
        private const int WaitMilliseconds = 500;

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
            Stopwatch.StartNew().Wait(WaitMilliseconds);
            return input;
        }

        protected abstract Queue<T> GenerateCommands();
    }
}