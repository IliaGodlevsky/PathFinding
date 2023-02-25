using Shared.Collections;
using Shared.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console
{
    internal static class MessageTokens
    {
        public static readonly Guid MainUnit = Guid.NewGuid();
        public static readonly Guid Screen = Guid.NewGuid();
        public static readonly Guid ColorsChangeItem = Guid.NewGuid();
        public static readonly Guid PathfindingColors = Guid.NewGuid();
        public static readonly Guid PathColors = Guid.NewGuid();
        public static readonly Guid RangeColors = Guid.NewGuid();
        public static readonly Guid GraphColors = Guid.NewGuid();

        public static readonly ReadOnlyList<Guid> All;

        static MessageTokens()
        {
            var flags = BindingFlags.Public | BindingFlags.Static;
            All = typeof(MessageTokens).GetFields(flags)
                .Where(field => field.FieldType == typeof(Guid))
                .Select(field => (Guid)field.GetValue(null))
                .ToReadOnly();
        }
    }
}
