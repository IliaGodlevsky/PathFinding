using System;

namespace Pathfinding.ConsoleApp.ViewModel
{
    [Flags]
    internal enum ImportExportOptions
    {
        GraphOnly,
        WithRange,
        WithRuns
    }
}