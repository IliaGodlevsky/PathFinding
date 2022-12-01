using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    internal interface IVisualCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        void Execute(TVertex vertex);

        bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex);
    }
}
