using Commands.Interfaces;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Interface
{
    internal interface IConsoleKeyCommand<TExecute> : IExecutable<TExecute>, IExecutionCheck<ConsoleKey>
    {

    }
}