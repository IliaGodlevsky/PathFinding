using Shared.Collections;
using Shared.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console
{
    internal static class MessageTokens
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Static;

        public static readonly Guid MainViewModel = Guid.NewGuid();
        public static readonly Guid Screen = Guid.NewGuid();

        public static readonly ReadOnlyList<Guid> All;

        static MessageTokens()
        {
            All = typeof(MessageTokens).GetFields(Flags)
                .Where(field => field.FieldType == typeof(Guid))
                .Select(field => (Guid)field.GetValue(null))
                .ToReadOnly();
        }
    }
}
