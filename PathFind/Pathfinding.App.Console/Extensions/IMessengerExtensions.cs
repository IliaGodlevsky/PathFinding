using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IMessengerExtensions
    {
        public static void SendMany<T>(this IMessenger messenger,
            T data, params IToken[] tokens)
        {
            messenger.SendMessage(data, tokens);
        }

        public static void RegisterData<T>(this IMessenger messenger, object recipient,
            IToken token, Action<T> action)
        {
            messenger.Register<T>(recipient, token, action);
        }

        public static void RegisterGraph(this IMessenger messenger, object recipient,
            IToken token, Action<GraphMessage> action)
        {
            messenger.RegisterData(recipient, token, action);
        }

        public static void RegisterAlgorithmData<T>(this IMessenger messenger, object recipient,
            IToken token, Action<(PathfindingProcess, T)> action)
        {
            messenger.Register<(PathfindingProcess, T)>(recipient, token, action);
        }

        private static void SendMessage<TMessage>(this IMessenger messenger,
            TMessage msg, params IToken[] tokens)
        {
            foreach (var token in tokens)
            {
                messenger.Send(msg, token);
            }
        }
    }
}
