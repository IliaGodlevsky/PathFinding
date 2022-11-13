﻿using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ValueInput.ProgrammedInput
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
            ColorfulConsole.WriteLine(input);
            Wait.Wait();
            return input;
        }

        protected abstract Queue<T> GenerateCommands();
    }
}