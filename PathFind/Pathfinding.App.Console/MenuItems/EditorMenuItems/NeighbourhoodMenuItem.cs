using Pathfinding.App.Console.DataAccess.Services;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.EditorMenuItems
{
    [MediumPriority]
    internal sealed class NeighbourhoodMenuItem : NavigateThroughVerticesMenuItem
    {
        public NeighbourhoodMenuItem(IInput<ConsoleKey> keyInput, IService service)
            : base(keyInput, service)
        {

        }

        public override void Execute()
        {
            HashSet<ICoordinate> GetNeighbors(Vertex vertex)
            {
                return vertex.Neighbours.GetCoordinates().ToHashSet();
            }
            var neighbors = graph.ToDictionary(x => x.Position, GetNeighbors);
            base.Execute();
            var addedNeighbors = new Dictionary<int, int[]>();
            var removedNeighbors = new Dictionary<int, int[]>();
            foreach (var vertex in processed)
            {
                var areNeighbours = vertex.Neighbours.GetCoordinates().ToHashSet();
                var wereNeighbours = neighbors[vertex.Position];
                var added = areNeighbours.Except(wereNeighbours)
                    .Select(x => graph.Get(x).Id);
                var removed = wereNeighbours.Except(areNeighbours)
                    .Select(x => graph.Get(x).Id);
                addedNeighbors.Add(vertex.Id, added.ToArray());
                removedNeighbors.Add(vertex.Id, removed.ToArray());
            }
            service.AddNeighbors(addedNeighbors);
            service.RemoveNeighbors(removedNeighbors);
        }

        public override string ToString()
        {
            return Languages.ChangeNeighbourhood;
        }

        protected override VertexActions GetActions()
        {
            var activeVertex = new ActiveVertex();
            var exitAction = new ExitAction(activeVertex);
            var neighbourhoodAction = new NeighbourhoodAction(activeVertex);
            var actions = new (string, IVertexAction)[]
            {
                (nameof(Keys.DoNeighbourhoodAction), neighbourhoodAction),
                (nameof(Keys.ExitVerticesNavigating), exitAction),
                (nameof(Keys.LeaveVertexAction), exitAction)
            };
            return Array.AsReadOnly(actions);
        }
    }
}
