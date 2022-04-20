using GalaSoft.MvvmLight.Messaging;
using System.Threading.Tasks;

namespace WPFVersion.Extensions
{
    internal static class IMessengerExtensions
    {
        public static async Task SendAsync<TMessage>(this IMessenger self, TMessage message)
        {
            await Task.Run(() => self.Send(message));
        }

        public static void SendParallel<TMessage>(this IMessenger self, TMessage message)
        {
            Task.Run(() => self.Send(message));
        }
    }
}