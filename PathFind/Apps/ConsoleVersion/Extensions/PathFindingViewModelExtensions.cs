using Algorithm.Base;
using Common;
using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System.Collections.Generic;

namespace ConsoleVersion.Extensions
{
    internal static class PathFindingViewModelExtensions
    {
        public static IReadOnlyCollection<IConsoleKeyCommand<ValueTypeWrap<int>>> GetAttachedDelayTimeConsoleKeyCommands(this PathFindingViewModel model)
        {
            return model.GetAttached<IConsoleKeyCommand<ValueTypeWrap<int>>>();
        }

        public static IReadOnlyCollection<IConsoleKeyCommand<PathfindingAlgorithm>> GetAttachedPathfindingKeyCommands(this PathFindingViewModel model)
        {
            return model.GetAttached<IConsoleKeyCommand<PathfindingAlgorithm>>();
        }
    }
}
