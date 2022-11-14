using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(4)]
    internal sealed class SetSourceVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> unsetCommand;

        public SetSourceVertexCommand(PathfindingRangeAdapter<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            unsetCommand = new UnsetSourceVertexCommand<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            adapter.Source = vertex;
            Source.VisualizeAsSource();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.Source == null
                && adapter.CanBeInRange(vertex);
        }

        public void Undo()
        {
            unsetCommand.Execute(Source);
        }
    }
}