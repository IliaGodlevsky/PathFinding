﻿using CommunityToolkit.Mvvm.Messaging.Messages;
using Pathfinding.Service.Interface.Models.Read;
using System;
using System.Reactive;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class AsyncGraphUpdatedMessage : IAsyncMessage<Unit>
    {
        public GraphInformationModel Model { get; }

        public Action<Unit> Signal { get; set; } = unit => throw new InvalidOperationException();

        public AsyncGraphUpdatedMessage(GraphInformationModel model)
        {
            Model = model;
        }
    }
}
