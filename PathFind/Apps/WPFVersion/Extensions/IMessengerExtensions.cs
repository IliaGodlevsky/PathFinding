using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;

namespace WPFVersion.Extensions
{
    internal static class IMessengerExtensions
    {
        public static void SendMany<TMessage1, TMessage2>(this IMessenger messenger,
            TMessage1 message1, TMessage2 message2, Guid token)
        {
            messenger.Send(message1, token);
            messenger.Send(message2, token);
        }

        public static async Task SendAsync<TMessage>(this IMessenger self, TMessage message, object messageToken)
        {
            await Task.Run(() => self.Send(message, messageToken));
        }
    }
}