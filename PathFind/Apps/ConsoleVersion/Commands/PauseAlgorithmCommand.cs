using Common.Attrbiutes;
using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Commands
{
    [AttachedTo(typeof(PathFindingViewModel))]
    internal sealed class PauseAlgorithmCommand : IConsoleKeyCommand
    {
        public void Execute(PathFindingViewModel viewModel)
        {
            viewModel.CurrentAlgorithm.Pause();
        }

        public bool CanExecute(ConsoleKey key)
        {
            return key.IsOneOf(ConsoleKey.P);
        }
    }
}