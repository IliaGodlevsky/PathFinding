using System;
using System.Linq;

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
            everyone = new Lazy<Guid[]>(GetAllModelTokens, isThreadSafe: true);
        }

        private static Guid[] GetAllModelTokens()
        {
            return typeof(MessageTokens)
                .GetProperties()
                .Where(property => !property.Name.Equals(nameof(Everyone)))
                .Select(property => (Guid)property.GetValue(null))
                .ToArray();
        }

        private static readonly Lazy<Guid[]> everyone;
    }
}
