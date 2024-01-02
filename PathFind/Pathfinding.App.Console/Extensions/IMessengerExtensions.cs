using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IMessengerExtensions
    {
        public static void SendMany<T>(this IMessenger messenger,
            T message, params IToken[] tokens)
            where T : class
        {
            foreach (var token in tokens)
            {
                messenger.Send(message, token);
            }
        }

        public static void Register<TRecipient, TMessage>(this IMessenger messenger,
            TRecipient recipient, IToken token, Action<TMessage> action)
            where TRecipient : class
            where TMessage : class
        {
            messenger.Register<TRecipient, TMessage, IToken>(recipient, token, (r, m) => action(m));
        }

        public static void RegisterGraph<TRecipient>(this IMessenger messenger, TRecipient recipient,
            IToken token, Action<GraphMessage> action)
            where TRecipient : class
        {
            messenger.Register<TRecipient, GraphMessage, IToken>(recipient, token, (r, m) => action(m));
        }
    }
}
