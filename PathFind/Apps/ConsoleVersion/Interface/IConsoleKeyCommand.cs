using Commands.Interfaces;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Interface
{
    internal interface IConsoleKeyCommand : IExecutable<PathFindingViewModel>, IExecutionCheck<ConsoleKey>
    {

    }
}