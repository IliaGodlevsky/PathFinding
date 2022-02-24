using Commands.Interfaces;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Interface
{
    internal interface IConsoleKeyCommand : IExecutable<PathFindingViewModel>, ICanCheck<ConsoleKey>
    {

    }
}