using System;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.WPF._2D
{
    internal static class MessageTokens
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Static;

        public static readonly Guid CostChangeSubscription = Guid.NewGuid();
        public static readonly Guid CostColors = Guid.NewGuid();

        public static readonly Guid[] All;

        static MessageTokens()
        {
            All = typeof(MessageTokens).GetFields(Flags)
                .Where(field => field.FieldType == typeof(Guid))
                .Select(field => (Guid)field.GetValue(null))
                .ToArray();
        }
    }
}
