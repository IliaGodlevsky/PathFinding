using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;

namespace WPFVersion.Extensions
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

        public static async Task SendAsync<TMessage>(this IMessenger self, TMessage message, Guid messageToken)
        {
            await Task.Run(() => self.Send(message, messageToken));
        }
    }
}