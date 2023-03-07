using GalaSoft.MvvmLight.Messaging;
using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IMessengerExtensions
    {
        public static void SendMany<TMessage>(this IMessenger messenger, TMessage message, params Guid[] tokens)
        {
            foreach (var token in tokens)
            {
                messenger.Send(message, token);
            }
        }
    }
}
