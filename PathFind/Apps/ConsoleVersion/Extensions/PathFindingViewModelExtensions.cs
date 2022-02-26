using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System.Collections.Generic;

namespace ConsoleVersion.Extensions
{
    internal static class PathFindingViewModelExtensions
    {
        public static IReadOnlyCollection<IConsoleKeyCommand> GetAttachedConsoleKeyCommands(this PathFindingViewModel model)
        {
            return model.GetAttached<IConsoleKeyCommand>();
        }
    }
}
