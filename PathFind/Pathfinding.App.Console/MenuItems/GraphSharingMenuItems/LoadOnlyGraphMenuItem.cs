﻿using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class LoadGraphOnlyMenuItem(IMessenger messenger,
        IFilePathInput input,
        ISerializer<GraphSerializationDto> serializer,
        ILog log,
        IService service) : IMenuItem
    {
        private readonly IMessenger messenger = messenger;
        private readonly IInput<string> input = input;
        private readonly ISerializer<GraphSerializationDto> serializer = serializer;
        private readonly IService service = service;
        private readonly ILog log = log;

        public void Execute()
        {
            try
            {
                var path = input.Input();
                var dto = serializer.DeserializeFromFile(path);
                var read = service.AddGraph(dto);
                var count = service.GetGraphIds().Count;
                if (count == 1)
                {
                    var graph = service.GetGraph(read.Id);
                    var costRange = graph.First().Cost.CostRange;
                    var costMsg = new CostRangeChangedMessage(costRange);
                    messenger.Send(costMsg, Tokens.AppLayout);
                    var graphMsg = new GraphMessage(graph, read.Id);
                    messenger.SendMany(graphMsg, Tokens.Visual,
                        Tokens.AppLayout, Tokens.Main, Tokens.Common);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return Languages.LoadGraphOnly;
        }
    }
}
