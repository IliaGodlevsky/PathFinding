using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IMessengerExtensions
    {
        public static void SendData<T>(this IMessenger messenger,
            T data, params Tokens[] tokens)
        {
            messenger.SendMessage(data, tokens);
        }

        public static void SendAlgorithmData<T>(this IMessenger messenger,
            PathfindingProcess algorithm, T data, params Tokens[] tokens)
        {
            messenger.SendMessage((algorithm, data), tokens);
        }

        public static void RegisterData<T>(this IMessenger messenger, object recipient,
            object token, Action<T> action)
        {
            messenger.Register<T>(recipient, token, action);
        }

        public static void RegisterAction<T>(this IMessenger messenger, object recipient,
            object token, Action action)
        {
            messenger.Register<T>(recipient, token, _ => action());
        }

        public static void RegisterGraph(this IMessenger messenger, object recipient,
            Tokens token, Action<Graph2D<Vertex>> action)
        {
            messenger.RegisterData(recipient, token, action);
        }

        public static void RegisterAlgorithmData<T>(this IMessenger messenger, object recipient,
            object token, Action<(PathfindingProcess, T)> action)
        {
            messenger.Register<(PathfindingProcess, T)>(recipient, token, action);
        }

        private static void SendMessage<TMessage>(this IMessenger messenger,
            TMessage msg, params Tokens[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                messenger.Send(msg, tokens[i]);
            }
        }
    }
}
