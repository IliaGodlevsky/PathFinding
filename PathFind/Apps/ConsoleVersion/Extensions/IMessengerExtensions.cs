using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Enums;
using EnumerationValues.Extensions;
using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using GalaSoft.MvvmLight.Messaging;

namespace ConsoleVersion.Extensions
{
    internal static class IMessengerExtensions
    {
        private static IEnumValues<MessageTokens> Tokens { get; }

        static IMessengerExtensions()
        {
            Tokens = new EnumValuesWithoutIgnored<MessageTokens>();
        }

        public static IMessenger Forward<TMessage>(this IMessenger messenger, TMessage message, MessageTokens token)
        {
            Tokens.DisassembleToFlags(token).ForEach(value => messenger.Send(message, value));
            return messenger;
        }

        public static IMessenger Forward<TMessage>(this IMessenger messenger, MessageTokens token)
            where TMessage : new()
        {
            return messenger.Forward(new TMessage(), token);
        }
    }
}