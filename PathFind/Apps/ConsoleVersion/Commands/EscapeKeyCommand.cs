using Commands.Attributes;
using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Commands
{
    [AttachedTo(typeof(PathFindingViewModel))]
    internal sealed class EscapeKeyCommand : IConsoleKeyCommand
    {
        public bool CanExecute(ConsoleKey key)
        {
            return key.IsOneOf(ConsoleKey.Escape, ConsoleKey.End, ConsoleKey.E);
        }

        public void Execute(PathFindingViewModel model)
        {
            model.CurrentAlgorithm.Interrupt();
        }
    }
}