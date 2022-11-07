using Shared.Executable;

namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IVisualizationCommand : IExecutable<IVisualizable>, IExecutionCheck<IVisualizable>
    {

    }
}
