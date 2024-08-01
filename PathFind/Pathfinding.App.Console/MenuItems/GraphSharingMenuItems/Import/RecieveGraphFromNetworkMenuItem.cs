﻿using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Requests.Create;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    [LowPriority]
    internal sealed class ReceiveGraphFromNetworkMenuItem(IMessenger messenger,
        IInput<int> input,
        ISerializer<IEnumerable<GraphSerializationModel>> serializer,
        ILog log,
        IRequestService<Vertex> service)
        : ImportGraphFromNetworkMenuItem<GraphSerializationModel>(messenger, input, serializer, log, service)
    {
        protected override async Task AddImported(IEnumerable<GraphSerializationModel> imported,
            CancellationToken token)
        {
            var request = new CreateGraphsFromSerializationRequest()
            {
                Graphs = imported.ToList()
            };
            await service.CreateGraphsAsync(request, token);
        }

        protected override async Task<GraphModel<Vertex>> AddSingleImported(GraphSerializationModel imported,
            CancellationToken token)
        {
            var request = new CreateGraphFromSerializationRequest()
            {
                DimensionSizes = imported.DimensionSizes,
                Vertices = imported.Vertices,
                Name = imported.Name
            };
            return await service.CreateGraphAsync(request, token);
        }

        protected override async Task Post(GraphModel<Vertex> model,
            CancellationToken token)
        {
            await Task.CompletedTask;
        }

        public override string ToString() => Languages.RecieveGraph;
    }
}
