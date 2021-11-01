using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;

namespace WPFVersion3D.Extensions
{
    internal static class MessengerExtensions
    {
        public static async Task SendAsync<TMessage>(this IMessenger self, TMessage message, Guid messageToken)
        {
            await Task.Run(() => self.Send(message, messageToken));
        }
    }
}
