using Shared.Executable;

namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IVisualizationCommand<T> : IExecutable<T>, IExecutionCheck<T>
        where T : IVisualizable
    {

    }
}
