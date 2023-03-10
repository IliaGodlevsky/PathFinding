using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IMessengerExtensions
    {
        public static void SendData<TData>(this IMessenger messenger, TData data, params Guid[] tokens)
        {
            var msg = new DataMessage<TData>(data);
            tokens.ForEach(token => messenger.Send(msg, token));
        }

        public static void SendData<TData>(this IMessenger messenger, PathfindingProcess algorithm,
            TData data, params Guid[] tokens)
        {
            var msg = new AlgorithmMessage<TData>(algorithm, data);
            tokens.ForEach(token => messenger.Send(msg, token));
        }

        public static void RegisterData<TData>(this IMessenger messenger, object recipient,
            Guid token, Action<DataMessage<TData>> action)
        {
            messenger.Register<DataMessage<TData>>(recipient, token, action);
        }

        public static void RegisterGraph(this IMessenger messenger, object recipient,
            Guid token, Action<DataMessage<Graph2D<Vertex>>> action)
        {
            messenger.RegisterData<Graph2D<Vertex>>(recipient, token, action);
        }

        public static void RegisterAlgorithmData<TData>(this IMessenger messenger, object recipient,
            Guid token, Action<AlgorithmMessage<TData>> action)
        {
            messenger.Register<AlgorithmMessage<TData>>(recipient, token, action);
        }
    }
}
