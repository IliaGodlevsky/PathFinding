using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class SaveRangeMenuItem : IConditionedMenuItem
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;
        private readonly IPathfindingRange<Vertex> range;
        private readonly IFilePathInput input;
        private readonly ILog log;

        public SaveRangeMenuItem(ISerializer<IEnumerable<ICoordinate>> serializer, 
            IPathfindingRange<Vertex> range, IFilePathInput input, ILog log)
        {
            this.serializer = serializer;
            this.range = range;
            this.input = input;
            this.log = log;
        }

        public bool CanBeExecuted()
        {
            return range.HasSourceAndTargetSet();
        }

        public async void Execute()
        {
            try
            {
                string path = input.Input();
                var coordinates = range.GetCoordinates().ToArray();
                await serializer.SerializeToFileAsync(coordinates, path);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString() => "Save range";
    }
}
