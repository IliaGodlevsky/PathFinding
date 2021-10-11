using Common.Extensions;
using System;

namespace ConsoleVersion
{
    internal static class MessageTokens
    {
        public static Guid[] Everyone => everyone.Value;
        public static Guid MainModel { get; }
        public static Guid MainView { get; }

        static MessageTokens()
        {
            MainModel = Guid.NewGuid();
            MainView = Guid.NewGuid();
            everyone = new Lazy<Guid[]>(GetEveryoneTokens, isThreadSafe: true);
        }

        private static Guid[] GetEveryoneTokens()
        {
            return typeof(MessageTokens).GetValuesOfStaticClassProperties<Guid>(nameof(Everyone));
        }

        private static readonly Lazy<Guid[]> everyone;
    }
}
