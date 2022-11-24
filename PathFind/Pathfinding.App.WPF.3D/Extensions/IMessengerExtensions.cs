using GalaSoft.MvvmLight.Messaging;
using System.Threading.Tasks;

namespace Pathfinding.App.WPF._3D.Extensions
{
    internal static class IMessengerExtensions
    {
        public static async Task SendAsync<TMessage>(this IMessenger self, TMessage message)
        {
            await Task.Run(() => self.Send(message));
        }
    }
}
