using GalaSoft.MvvmLight.Messaging;
using System;

namespace ConsoleVersion.Extensions
{
    internal static class IMessengerExtensions
    {
        public static void SendMany<TMessage>(this IMessenger messenger,
            TMessage message, params Guid[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                messenger.Send(message, tokens[i]);
            }
        }
    }
}