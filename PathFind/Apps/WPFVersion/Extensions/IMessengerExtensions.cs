using GalaSoft.MvvmLight.Messaging;

namespace WPFVersion.Extensions
{
    internal static class IMessengerExtensions
    {
        public static void SendMany<TMessage>(this IMessenger messenger, TMessage message, params object[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                messenger.Send(message, tokens[i]);
            }
        }
    }
}