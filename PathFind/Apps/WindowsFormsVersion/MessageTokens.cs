using System;
using System.Linq;

namespace WindowsFormsVersion
{
    class MessageTokens
    {
        public static Guid[] AllModels => allModels.Value;
        public static Guid MainModel { get; }

        static MessageTokens()
        {
            MainModel = Guid.NewGuid();
            allModels = new Lazy<Guid[]>(GetAllModelTokens, isThreadSafe: true);
        }

        private static Guid[] GetAllModelTokens()
        {
            return typeof(MessageTokens)
                .GetProperties()
                .Where(property => !property.Name.Equals(nameof(AllModels)))
                .Select(property => (Guid)property.GetValue(null))
                .ToArray();
        }

        private static readonly Lazy<Guid[]> allModels;
    }
}
