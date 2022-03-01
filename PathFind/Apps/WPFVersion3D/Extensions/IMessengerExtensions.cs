using Common.Extensions.EnumerableExtensions;
using EnumerationValues.Extensions;
using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using GalaSoft.MvvmLight.Messaging;
using System.Threading.Tasks;
using WPFVersion3D.Enums;

namespace WPFVersion3D.Extensions
{
    internal static class IMessengerExtensions
    {
        private static IEnumValues<MessageTokens> Tokens { get; }

        static IMessengerExtensions()
        {
            Tokens = new EnumValuesWithoutIgnored<MessageTokens>();
        }

        public static async Task ForwardAsync<TMessage>(this IMessenger self,
            TMessage message, MessageTokens messageToken)
        {
            await Task.Run(() => self.Forward(message, messageToken));
        }

        public static void Forward<TMessage>(this IMessenger messenger, TMessage message, MessageTokens token)
        {
            Tokens.DisassembleToFlags(token).ForEach(value => messenger.Send(message, value));
        }
    }
}
