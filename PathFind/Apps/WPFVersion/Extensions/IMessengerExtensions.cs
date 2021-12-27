using Common.Extensions.EnumerableExtensions;
using EnumerationValues.Extensions;
using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using GalaSoft.MvvmLight.Messaging;
using System;
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

        public static async Task ForwardAsync<TMessage>(this IMessenger self, TMessage message, MessageTokens messageToken)
        {
            await Task.Run(() => self.Forward(message, messageToken));
        }

        /// <summary>
        /// Sends a message to recipient(s), registered under the <paramref name="token"/>(s)
        /// </summary>
        /// <typeparam name="TMessage">A message type that should be sent</typeparam>
        /// <param name="messenger">A messenger, that sends the <paramref name="message"/></param>
        /// <param name="message">A message, that should be sent</param>
        /// <param name="token">A message token, that channels <paramref name="message"/> delivering</param>
        /// <remarks>You can use '|' to combine message tokens and send <paramref name="message"/> 
        /// to several recipients</remarks>
        public static IMessenger Forward<TMessage>(this IMessenger messenger, TMessage message, MessageTokens token)
        {
            Tokens.BreakIntoFlags(token).ForEach(value => messenger.Send(message, value));
            return messenger;
        }
    }
}