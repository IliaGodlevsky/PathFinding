﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(6)]
    internal sealed class SetTargetVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> undoCommand;

        public SetTargetVertexCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            undoCommand = new UnsetTargetVertexCommand<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            pathfindingRange.Target = vertex;
            Target.VisualizeAsTarget();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Source != null
                && pathfindingRange.Target == null
                && pathfindingRange.CanBeInRange(vertex);
        }

        public void Undo()
        {
            undoCommand.Execute(Target);
        }
    }
}