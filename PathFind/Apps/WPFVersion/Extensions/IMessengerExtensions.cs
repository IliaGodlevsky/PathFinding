using Common.Extensions.EnumerableExtensions;
using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
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

        public static void ForwardMany<TMessage1, TMessage2>(this IMessenger messenger,
            TMessage1 message1, TMessage2 message2, MessageTokens token)
        {
            messenger.Forward(message1, token);
            messenger.Forward(message2, token);
        }

        public static async Task ForwardAsync<TMessage>(this IMessenger self, TMessage message, MessageTokens messageToken)
        {
            await Task.Run(() => self.Forward(message, messageToken));
        }

        public static void Forward<TMessage>(this IMessenger messenger,
            TMessage message, MessageTokens token)
        {
            Tokens.Values
                .Where(value => token.HasFlag(value))
                .ForEach(value => messenger.Send(message, value));
        }
    }
}