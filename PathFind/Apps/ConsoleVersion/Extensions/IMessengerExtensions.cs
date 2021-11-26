using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Enums;
using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;

namespace ConsoleVersion.Extensions
{
    internal static class IMessengerExtensions
    {
        private static IEnumValues<MessageTokens> Tokens { get; }

        static IMessengerExtensions()
        {
            Tokens = new EnumValuesWithoutIgnored<MessageTokens>();
        }

        /// <summary>
        /// Sends message to recipient(s), registered under the <paramref name="token"/>(s)
        /// </summary>
        /// <typeparam name="TMessage">A message type that should be sent</typeparam>
        /// <param name="messenger">A messenger, that sends the <paramref name="message"/></param>
        /// <param name="message">A message, that should be sent</param>
        /// <param name="token">A message token, that channels <paramref name="message"/> delivering</param>
        /// <remarks>You can use '|' to combine message tokens and send <paramref name="message"/> 
        /// to several recipients</remarks>
        public static void Forward<TMessage>(this IMessenger messenger, TMessage message, MessageTokens token)
        {
            bool IsPartOfToken(MessageTokens value) => token.HasFlag(value);
            void SendMessageByTokenPart(MessageTokens value) => messenger.Send(message, value);
            Tokens.Values.Where(IsPartOfToken).ForEach(SendMessageByTokenPart);
        }
    }
}