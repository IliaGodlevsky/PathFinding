using Commands.Interfaces;
using System;

namespace ConsoleVersion.Interface
{
    internal interface IConsoleKeyCommand<TExecute> : IExecutable<TExecute>, IExecutionCheck<ConsoleKey>
    {

    }
}