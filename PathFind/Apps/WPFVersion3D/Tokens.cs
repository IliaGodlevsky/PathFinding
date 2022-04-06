using Common.Extensions.EnumerableExtensions;
using System;
using System.Linq;
using System.Reflection;

namespace WPFVersion3D
{
    internal static class Tokens
    {
#pragma warning disable CS0649
        public static readonly Guid MainModel;
        public static readonly Guid AlgorithmStatisticsModel;
        public static readonly Guid PathfindingModel;
        public static readonly Guid StretchAlongAxisModel;
        public static readonly Guid GraphFieldModel;
        public static readonly Guid SaveGraphViewModel;
        public static readonly Guid ClearColorsModel;
        public static readonly Guid LoadGraphModel;
        public static readonly Guid InterruptAllAlgorithmsModel;
        public static readonly Guid CreateGraphModel;
        public static readonly Guid ClearGraphModel;
        public static readonly Guid ChangeOpacityModel;
#pragma warning restore CS0649

        public static Guid[] Everyone { get; }

        static Tokens()
        {
            Everyone = typeof(Tokens)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .ForAll(field => field.SetValue(null, Guid.NewGuid()))
                .Select(field => (Guid)field.GetValue(null))
                .ToArray();
        }
    }
}
