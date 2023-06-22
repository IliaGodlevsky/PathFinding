using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighestPriority]
    internal sealed class EnterPathfindingRangeMenuItem : SwitchVerticesMenuItem
    {
        private readonly IPathfindingRangeBuilder<Vertex> builder;

        public EnterPathfindingRangeMenuItem(IReadOnlyCollection<(string, IVertexAction)> actions,
            IPathfindingRangeBuilder<Vertex> builder,
            IUnitOfWork unitOfWork,
            IInput<ConsoleKey> keyInput)
            : base(actions, keyInput, unitOfWork)
        {
            this.builder = builder;
        }

        public override bool CanBeExecuted()
        {
            return graph.Graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        protected override void PostExecute()
        {
            graph.Range = builder.Range.GetCoordinates().ToArray();
            base.PostExecute();
        }

        public override string ToString()
        {
            return Languages.EnterPathfindingRange;
        }
    }
}
