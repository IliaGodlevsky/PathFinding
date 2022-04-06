using Common.Extensions.EnumerableExtensions;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;

namespace WPFVersion3D.Extensions
{
    internal static class IMessengerExtensions
    {
        public static async Task ForwardAsync<TMessage>(this IMessenger self, TMessage message, params Guid[] tokens)
        {
            await Task.Run(() => self.Forward(message, tokens));
        }

        public static IMessenger Forward<TMessage>(this IMessenger messenger, TMessage message, params Guid[] tokens)
        {
            tokens.ForEach(token => messenger.Send(message, token));
            return messenger;
        }
    }
}
