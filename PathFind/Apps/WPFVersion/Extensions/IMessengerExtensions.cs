using Common.Extensions.EnumerableExtensions;
using EnumerationValues.Extensions;
using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using GalaSoft.MvvmLight.Messaging;
using System.Threading.Tasks;
using WPFVersion.Enums;

namespace WPFVersion.Extensions
{
    internal static class IMessengerExtensions
    {
        private static IEnumValues<MessageTokens> Tokens { get; }

        static IMessengerExtensions()
        {
            Tokens = new EnumValuesWithoutIgnored<MessageTokens>();
        }

        public static async Task ForwardAsync<TMessage>(this IMessenger self, TMessage message, MessageTokens messageToken)
        {
            await Task.Run(() => self.Forward(message, messageToken));
        }

        public static void ForwardParallel<TMessage>(this IMessenger self, TMessage message, MessageTokens messageToken)
        {
            Task.Run(() => self.Forward(message, messageToken));
        }

        public static IMessenger Forward<TMessage>(this IMessenger messenger, TMessage message, MessageTokens token)
        {
            Tokens.DisassembleToFlags(token).ForEach(value => messenger.Send(message, value));
            return messenger;
        }
    }
}