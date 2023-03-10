using Shared.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console
{
    internal static class Tokens
    {
#pragma warning disable CS0649
        public static readonly Guid Main;
        public static readonly Guid Screen;
        public static readonly Guid History;
        public static readonly Guid Statistics;
        public static readonly Guid Visualization;
        public static readonly Guid Graph;
        public static readonly Guid Pathfinding;
        public static readonly Guid Path;
        public static readonly Guid Range;
        public static readonly Guid Common;
#pragma warning restore CS0649

        public static readonly Guid[] All;

        static Tokens()
        {
            var flags = BindingFlags.Public | BindingFlags.Static;
            All = typeof(Tokens).GetFields(flags)
                .Where(field => field.FieldType == typeof(Guid))
                .ForEach(field => field.SetValue(null, Guid.NewGuid()))
                .Select(field => (Guid)field.GetValue(null))
                .ToArray();
        }
    }
}
