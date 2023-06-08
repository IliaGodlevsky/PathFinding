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
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class SendRangeMenuItem : IConditionedMenuItem
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer;
        private readonly IPathfindingRange<Vertex> range;
        private readonly IInput<(string Host, int Port)> input;
        private readonly ILog log;

        public SendRangeMenuItem(ISerializer<IEnumerable<ICoordinate>> serializer,
            IPathfindingRange<Vertex> range, IInput<(string Host, int Port)> input, ILog log)
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
                var path = input.Input();
                var coordinates = range.GetCoordinates().ToArray();
                await serializer.SerializeToNetworkAsync(coordinates, path);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString() => "Send range";
    }
}
