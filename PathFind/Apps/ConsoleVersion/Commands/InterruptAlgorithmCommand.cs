using Algorithm.Base;
using Common.Attrbiutes;
using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Commands
{
    [AttachedTo(typeof(PathFindingViewModel))]
    internal sealed class InterruptAlgorithmCommand : IConsoleKeyCommand<PathfindingAlgorithm>
    {
        public void Execute(PathfindingAlgorithm algorithm)
        {
            algorithm.Interrupt();
        }

        public bool CanExecute(ConsoleKey key)
        {
            return key.IsOneOf(ConsoleKey.Escape, ConsoleKey.End);
        }
    }
}